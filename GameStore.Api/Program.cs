using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Load optional secrets.json config file
builder.Configuration
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// Get connection string from config and register DbContext
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Configure Kestrel to listen on the port provided by Render (environment variable PORT)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var port = Environment.GetEnvironmentVariable("PORT");
    if (!string.IsNullOrEmpty(port) && int.TryParse(port, out var portNumber))
    {
        serverOptions.ListenAnyIP(portNumber);
    }
    else
    {
        // Fallback port for local development
        serverOptions.ListenAnyIP(5000);
    }
});

var app = builder.Build();

// Map your API endpoints
app.MapGamesEndpoints();
app.MapGenresEndpoints();

// Run migrations (apply EF Core migrations)
await app.MigrateDbAsync();

// Start the app
app.Run();
