using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityUser = GlossaryEng.Auth.Data.Entities.IdentityUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string databaseConnection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DevAuthDatabase")
    : "SomeConnectionString";

// Add DataBase connection
builder.Services.AddDbContext<UsersDbContext>(
    options => options.UseNpgsql(databaseConnection));


// Add only user management system
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();