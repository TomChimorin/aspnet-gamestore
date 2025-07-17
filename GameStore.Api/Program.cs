using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Load secrets
builder.Configuration.AddJsonFile("secrets.json", optional: true);

builder.Services.AddSqlite<GameStoreContext>(
    builder.Configuration.GetConnectionString("GameStore"));

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

var port = Environment.GetEnvironmentVariable("PORT") ?? "unknown";
Console.WriteLine($"✅ ASP.NET Core is listening on port: {port}");

app.Run();
