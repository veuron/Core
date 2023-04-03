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
            // добавляем в бд
            db.Users.AddRange(new User() { Name = name, Age = Convert.ToInt32(age)});
            db.SaveChanges();

            var users = db.Users.ToList();
            foreach (var user in users)
                Console.WriteLine(user.Name);
        }
        await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
    }
    else
    {
        await context.Response.SendFileAsync("html/index.html");
    }
});

app.Run();
