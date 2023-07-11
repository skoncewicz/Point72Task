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
    
    private readonly FindBookQuery _findBookQuery;
    private readonly InvertWords _invertWords;

    public GetInvertedBook(FindBookQuery findBookQuery, InvertWords invertWords)
    {
        _findBookQuery = findBookQuery;
        _invertWords = invertWords;
    }

    private async Task<IResult> Execute(long id)
    {
        var book = await _findBookQuery.ExecuteAsync(id);
        
        if(book is null)
            return Results.NotFound();

        var invertedBook = new BookDto(
            book.Id,
            _invertWords.Invert(book.Title),
            book.Description,
            string.Empty
        );
        
        return Results.Json(invertedBook);
    }

    public record BookDto (long Id, string Title, string? Description, string Author);
}