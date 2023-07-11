using Microsoft.EntityFrameworkCore;
using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.Queries;

public class EnsureDbConnectionQuery
{
    private readonly LibraryDbContext _context;
    private readonly ILogger<EnsureDbConnectionQuery> _logger;

    public EnsureDbConnectionQuery(LibraryDbContext context, ILogger<EnsureDbConnectionQuery> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    /// <summary>
    /// Simple query to ensure that the database connection is working and that tables are mapped for basic queries.
    /// </summary>
    public async Task Execute()
    {
        _logger.LogInformation("Ensuring database connection...");
        await _context.Authors.FirstOrDefaultAsync();
        await _context.Books.FirstOrDefaultAsync();
        await _context.Users.FirstOrDefaultAsync();
        await _context.BooksTaken.FirstOrDefaultAsync();
        _logger.LogInformation("Database connection OK!");
    }
}