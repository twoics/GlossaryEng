using System.Text;
using GlossaryEngApi.Exceptions;
using GlossaryEngApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string databaseConnection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DevDataBase")
    : "SomeConnectionString";

AuthConfiguration? authConfiguration
    = builder.Configuration.GetSection("Authentication").Get<AuthConfiguration>();

if (authConfiguration is null)
{
    throw new ConfigureStringException(
        "Unable to create an AuthConfiguration instance from the configuration. Check fields and try again");
}

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authConfiguration.Issuer,
                ValidateAudience = true,
                ValidAudience = authConfiguration.Audience,
                ValidateLifetime = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.AccessTokenSecret)),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();