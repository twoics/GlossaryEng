var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string databaseConnection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DockerDataBase")
    : "SomeConnectionString";

System.Console.WriteLine(databaseConnection);

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();