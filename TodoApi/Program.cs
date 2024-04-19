using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Agrega los controladores de la API
builder.Services.AddControllers();

//Agrega enpoints
builder.Services.AddEndpointsApiExplorer();

//Agrega el Swagger
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<TodoContext>(
        // Use SQL Server
        // opt.UseSqlServer(builder.Configuration.GetConnectionString("TodoContext"));

        // Carga en memoria la base
        opt => opt.UseInMemoryDatabase("TodoList")
    )
    .AddEndpointsApiExplorer()
    .AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion

app.UseAuthorization();
app.MapControllers();
app.Run();