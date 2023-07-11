using Microsoft.EntityFrameworkCore;
using Point72.LibraryApi.Database;
using Point72.LibraryApi.Endpoints;
using Point72.LibraryApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Configure database
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LibraryDbContext");
    
    options.UseSqlServer(connectionString);
});

// Register modules
QueriesModule.Register(builder.Services);
EndpointsModule.Register(builder.Services);
    
var app = builder.Build();

// Configure endpoints
app.MapGet("/", async () =>
{
    // Try out queries to make sure the connection string and basic mappings are ok
    using (var scope = app.Services.CreateScope())
    {
        var ensureDbConnection = scope.ServiceProvider.GetRequiredService<EnsureDbConnectionQuery>();
        await ensureDbConnection.Execute();
    }
    
    return "Application started!";
});
GetBook.MapEndpoint(app);

// Start the application
app.Run();