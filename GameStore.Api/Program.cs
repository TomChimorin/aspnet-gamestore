using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Load secrets if any
builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// Get connection string and add EF Core SQLite context
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Configure Kestrel to listen on Render port (or fallback to 8080)
builder.WebHost.ConfigureKestrel(options =>
{
    var portEnv = Environment.GetEnvironmentVariable("PORT");
    if (int.TryParse(portEnv, out var port))
    {
        options.ListenAnyIP(port);
    }
    else
    {
        options.ListenAnyIP(8080); // fallback
    }
});

var app = builder.Build();

// Map endpoints
app.MapGamesEndpoints();
app.MapGenresEndpoints();

// Run migrations on startup
await app.MigrateDbAsync();

app.Run();
