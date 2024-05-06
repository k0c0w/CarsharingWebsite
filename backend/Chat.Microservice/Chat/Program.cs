using Chat.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();
await app.ConfigureAsync();

app.Run();
