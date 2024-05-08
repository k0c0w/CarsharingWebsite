using Chat.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();
app.Configure();
await app.TryMigrateDatabaseAsync();

app.Run();
