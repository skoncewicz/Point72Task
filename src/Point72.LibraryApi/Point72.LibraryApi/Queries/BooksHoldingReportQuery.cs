using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.CompilerServices;
using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.Queries;

public class BooksHoldingReportQuery
{
    private readonly LibraryDbContext _context;

    public BooksHoldingReportQuery(LibraryDbContext context)
    {
        _context = context;
    }

    public record UserReport(User User, List<long> BookIds, int TotalDays);
    
    public async Task<List<UserReport>> Execute()
    {
        var usersWithBooks = await _context.Users
            .Include(u => u.BooksTaken)
            .ThenInclude(bt => bt.Book)
            .Select(u => new
            {
                User = u,
                Books = u.BooksTaken.Select(bt => new
                {
                    Id = bt.Book.Id,
                    TakenAt = bt.DateTaken
                })
            })
            .ToListAsync();
        
        var report = usersWithBooks.Select(x => new UserReport(
            x.User,
            x.Books.Select(b => b.Id).ToList(),
            x.Books.Sum(b => (DateTime.Now - b.TakenAt).Days)
        )).ToList();

        return report;
    }
}