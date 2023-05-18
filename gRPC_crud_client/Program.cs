using Grpc.Net.Client;
using gRPC_CRUD;

// создаем канал для обмена сообщениями с сервером
// параметр - адрес сервера gRPC
using var channel = GrpcChannel.ForAddress("https://localhost:7186");

// создаем клиент
var client = new UserService.UserServiceClient(channel);

// получение списка
ListReply users = await client.ListUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());

foreach (var user in users.Users)
{
    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}