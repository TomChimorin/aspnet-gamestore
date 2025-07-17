using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Load secrets if any
builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// Add EF Core with SQLite
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Bind to PORT provided by Render
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var portStr = Environment.GetEnvironmentVariable("PORT");
    var port = string.IsNullOrEmpty(portStr) ? 8080 : int.Parse(portStr);
    serverOptions.ListenAnyIP(port);
});

var app = builder.Build();

// Register endpoints
app.MapGamesEndpoints();
app.MapGenresEndpoints();

// Apply EF migrations
await app.MigrateDbAsync();

// KEEP THE APP ALIVE
app.Run();
