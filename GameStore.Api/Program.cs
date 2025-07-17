using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Load optional secrets.json if you use it for config
builder.Configuration
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// Get connection string
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Get port from environment or default to 8080
var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
if (!int.TryParse(portEnv, out var port))
{
    port = 8080;
}

// Configure Kestrel to listen on the port Render provides
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(port);
});

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

// Apply EF migrations on startup
await app.MigrateDbAsync();

app.Run();
