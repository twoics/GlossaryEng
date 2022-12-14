using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Exceptions;
using GlossaryEng.Auth.Models.AuthConfiguration;
using GlossaryEng.Auth.Models.EmailConfiguration;
using GlossaryEng.Auth.Services.Authenticator;
using GlossaryEng.Auth.Services.EmailSender;
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


// Will use only user management system
builder.Services.AddIdentity<UserDb, IdentityRole>(options =>
        options.User.RequireUniqueEmail = true
    )
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();


// Add auth configuration
AuthenticationConfiguration? authenticationConfiguration
    = builder.Configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();

if (authenticationConfiguration is null)
{
    throw new ConfigureStringException(
        "Unable to create an AuthenticationConfiguration instance from the configuration. Check fields and try again");
}

EmailConfiguration? emailConfiguration
    = builder.Configuration.GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();

if (emailConfiguration is null)
{
    throw new ConfigureStringException(
        "Unable to create an EmailConfiguration instance from the configuration. Check fields and try again");
}

// Dependency injection
builder.Services.AddSingleton(emailConfiguration);
builder.Services.AddSingleton(authenticationConfiguration);

builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
builder.Services.AddSingleton<ITokenValidator, TokenValidator>();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IAuthenticator, Authenticator>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();