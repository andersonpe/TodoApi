using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Services;
using TodoApi.Domain.Interfaces;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.UnitsOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Aplica migrations automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();   // Mantido em todos os ambientes para facilitar a avaliação (inclusive no Docker)
app.MapControllers();
app.Run();

public partial class Program { }
