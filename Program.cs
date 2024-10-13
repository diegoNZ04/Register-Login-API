using Microsoft.EntityFrameworkCore;
using RegisterLoginAPI.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Adiciona o contexto de banco de dados ao contêiner de Injeção de Dependência
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("UserList")); // Especifica que o contexto de banco de dados usará um banco de dados em memória.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
