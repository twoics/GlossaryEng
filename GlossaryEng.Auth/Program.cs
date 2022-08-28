using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.AuthConfiguration;
using GlossaryEng.Auth.Models.RefreshTokensRepository;
using GlossaryEng.Auth.Models.TokenGenerator;
using GlossaryEng.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

string databaseConnection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DevAuthDatabase")
    : "SomeConnectionString";

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
AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
builder.Configuration.Bind("Authentication", authenticationConfiguration);

builder.Services.AddSingleton(authenticationConfiguration);
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();