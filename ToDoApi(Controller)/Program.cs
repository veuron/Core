//Microsoft.EntityFrameworkCore.InMemory
using Microsoft.EntityFrameworkCore;
using ToDoApi_Controller_.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//контекст БД
builder.Services.AddDbContext<ToDoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
