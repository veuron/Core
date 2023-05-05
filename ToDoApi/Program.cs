using Microsoft.EntityFrameworkCore;
using ToDoApi;

var builder = WebApplication.CreateBuilder(args);

//Подключили контекст работы с хранилищем
builder.Services.AddDbContext<ToDoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

//Страница для вывода ошибок работы с БД
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

//группировка запроса для экономии кода
RouteGroupBuilder todoItems = app.MapGroup("/todoitems");

//маппинг запросов на спец методы
todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompleteTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);
app.Run();

//Получение всех записей
static async Task<IResult> GetAllTodos(ToDoDb db)
{
    return TypedResults.Ok(await db.ToDos.Select(x => new TodoItemDTO(x)).ToArrayAsync());
}

//Получение только выполненных записей
static async Task<IResult> GetCompleteTodos(ToDoDb db)
{
    return TypedResults.Ok(await db.ToDos.Where(t => t.IsComplete).Select(x => new TodoItemDTO(x)).ToListAsync());
}

//Получение конкретной записи по id
static async Task<IResult> GetTodo(int id, ToDoDb db)
{
    return await db.ToDos.FindAsync(id)
        is ToDo todo
            ? TypedResults.Ok(new TodoItemDTO(todo))
            : TypedResults.NotFound();
}

//Создание записи 
static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, ToDoDb db)
{
    var todoItem = new ToDo
    {
        IsComplete = todoItemDTO.IsComplete,
        Name = todoItemDTO.Name
    };

    db.ToDos.Add(todoItem);
    await db.SaveChangesAsync();

    todoItemDTO = new TodoItemDTO(todoItem);

    return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItemDTO);
}

//Изменение записи
static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, ToDoDb db)
{
    var todo = await db.ToDos.FindAsync(id);

    if (todo is null) return TypedResults.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsComplete = todoItemDTO.IsComplete;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

//Удаление
static async Task<IResult> DeleteTodo(int id, ToDoDb db)
{
    if (await db.ToDos.FindAsync(id) is ToDo todo)
    {
        db.ToDos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.Ok(todo);
    }

    return TypedResults.NotFound();
}