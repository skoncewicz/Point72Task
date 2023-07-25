using Microsoft.AspNetCore.Mvc;
using Point72.LibraryApi.Queries;

namespace Point72.LibraryApi.Endpoints;

// 2.	Endpoint to invert words in Title of the given book (/api/invertwords).
// Book is identified by ID. When called, service must invert all words in the Title of the book (words are sequences of
// characters and numbers separated by spaces or other signs like commas, semicolons etc.) 
// Returns: book object
public class GetInvertedBook
{
    public static void MapEndpoint(WebApplication app) => app.MapGet("/api/invertwords/{id}", 
        ([FromServices] GetInvertedBook handler, long id) => handler.Execute(id)
    );
    
    public record BookDto (long Id, string Title, string? Description, AuthorDto Author);
    public record AuthorDto (long Id, string FirstName, string LastName);
    private async Task<IResult> Execute(long id)
    {
        var book = await _findBookQuery.ExecuteAsync(id);
        
        if(book is null)
            return Results.NotFound();

        var invertedBook = new BookDto(
            book.Id,
            _invertWords.Invert(book.Title),
            book.Description,
            new AuthorDto(book.Author.Id, book.Author.FirstName, book.Author.LastName)
        );
        
        return Results.Json(invertedBook);
    }
    
    private readonly FindBookQuery _findBookQuery;
    private readonly IInvertWords _invertWords;

    public GetInvertedBook(FindBookQuery findBookQuery, IInvertWords invertWords)
    {
        _findBookQuery = findBookQuery;
        _invertWords = invertWords;
    }

}