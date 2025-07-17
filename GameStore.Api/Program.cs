using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Configure Kestrel to listen on Render's port
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var port = Environment.GetEnvironmentVariable("PORT");
    if (!string.IsNullOrEmpty(port) && int.TryParse(port, out var portNumber))
    {
        serverOptions.ListenAnyIP(portNumber);
    }
});

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

try
{
    await app.MigrateDbAsync();
    Console.WriteLine("Database migration succeeded.");
}
catch (Exception ex)
{
    Console.WriteLine($"Database migration failed: {ex.Message}");
    throw; // or handle appropriately
}

app.Run();
