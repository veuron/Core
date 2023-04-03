using MVC.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();


var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // если обращение идет по адресу "/postuser" , получаем данные формы
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];

        using (var db = new UserContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            User user1 = new User { Name = "Tom", Age = 33 };
            User user2 = new User { Name = "Alice", Age = 26 };

            // добавляем их в бд
            db.Users.AddRange(user1, user2);
            db.SaveChanges();
        }
        await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
    }
    else
    {
        await context.Response.SendFileAsync("html/index.html");
    }
});

app.Run();
