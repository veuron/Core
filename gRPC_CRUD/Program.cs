using gRPC_CRUD.Services;

var builder = WebApplication.CreateBuilder(args);

// добавляем сервисы для работы с gRPC
builder.Services.AddGrpc();

var app = builder.Build();

// настраиваем обработку HTTP-запросов
app.MapGrpcService<UserApiService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client...");

app.Run();