using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
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

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();