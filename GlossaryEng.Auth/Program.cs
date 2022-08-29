using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Exceptions;
using GlossaryEng.Auth.Models.AuthConfiguration;
using GlossaryEng.Auth.Services.Authenticator;
using GlossaryEng.Auth.Services.Mapper;
using GlossaryEng.Auth.Services.RefreshTokensRepository;
using GlossaryEng.Auth.Services.TokenGenerator;
using GlossaryEng.Auth.Services.TokenValidator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

string databaseConnection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DevAuthDatabase")
    : "SomeConnectionString";

if (string.IsNullOrEmpty(databaseConnection))
{
    throw new ConfigureStringException(
        "The db connection string contains an empty string, or has the value null");
}

// Add DataBase connection
builder.Services.AddDbContext<UsersDbContext>(
    options => options.UseNpgsql(databaseConnection));


// Add only user management system
builder.Services.AddIdentityCore<UserDb>(options =>
        options.User.RequireUniqueEmail = true
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>();

// Add auth configuration
AuthenticationConfiguration authenticationConfiguration
    = builder.Configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();

if (authenticationConfiguration is null)
{
    throw new ConfigureStringException(
        "Unable to create an AuthenticationConfiguration instance from the configuration. Check fields and try again");
}

builder.Services.AddSingleton(authenticationConfiguration);
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();
builder.Services.AddTransient<ITokenValidator, TokenValidator>();
builder.Services.AddSingleton<IAuthenticator, Authenticator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();