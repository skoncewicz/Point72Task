using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point72.LibraryApi.Queries;

namespace Point72.LibraryApi.Endpoints;

//1.	Endpoint to search books (/api/search). The service can search books using following criteria:
// a)	by author
// b)	by text in book’s title or description
// c)	by user who’s currently holding the book
// d)	if more than one criterion specified - parameter defining how multiple criterions must be combined – either OR or AND condition.
// Results: list of books which meet criteria
public class GetBook
{
    public static void MapEndpoint(WebApplication app) => app.MapGet("/api/search",
        ([FromServices] GetBook handler, HttpRequest request, string? author, string? text, long? userId) 
            => handler.Execute(request, author, text, userId));
    
    private readonly SearchBooksQuery _searchBooksQuery;

    public GetBook(SearchBooksQuery searchBooksQuery)
    {
        _searchBooksQuery = searchBooksQuery;
    }
    
    public record BookDto (long Id, string Title, string? Description, string Author);
    public record AuthorDto (long Id, string FirstName, string LastName);

    public async Task<IResult> Execute(HttpRequest httpRequest, string? author, string? text, long? userId)
    {
        var acceptedParameters = new[] { "author", "text", "userid" };
        var unexpectedParameters = httpRequest.Query
            .Select(p => p.Key.ToLower())
            .Where(p => !acceptedParameters.Contains(p))
            .ToList();

        if (unexpectedParameters.Any())
            return Results.BadRequest("This endpoint does not accept following parameters: " +
                                      String.Join(", ", unexpectedParameters));

        var results = await _searchBooksQuery.ExecuteAsync(author, text, userId);
        var mappedToDto = await results.Select(b =>
            new BookDto(b.Id, b.Title, b.Description, "")
        ).ToListAsync();

        return Results.Json(mappedToDto);
    }
}