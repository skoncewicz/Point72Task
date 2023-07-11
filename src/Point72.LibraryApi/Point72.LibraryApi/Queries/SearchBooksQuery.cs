using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.Queries;

public class SearchBooksQuery
{
    private readonly LibraryDbContext _context;

    public SearchBooksQuery(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Book>> ExecuteAsync(string? author = null, string? text = null)
    {
        var query = _context.Books.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(author))
        {
            var authorLowercase = author.ToLower();
            
            query = query.Where(
                x => x.Author.FirstName.ToLower().Contains(authorLowercase)
                     || x.Author.LastName.ToLower().Contains(authorLowercase)
                     || x.Author.MiddleName.ToLower().Contains(authorLowercase)
            );
        }

        if (!string.IsNullOrWhiteSpace(text))
        {
            var textLowercase = text.ToLower();

            query = query.Where(
                x => x.Title.ToLower().Contains(textLowercase)
                     || x.Description.ToLower().Contains(textLowercase)
            );
        }

        return query;
    }
}