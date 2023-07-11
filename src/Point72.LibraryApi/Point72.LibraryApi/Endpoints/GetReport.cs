using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point72.LibraryApi.Queries;

namespace Point72.LibraryApi.Endpoints;

// 3.	Endpoint to generate report (/api/report) – list of users with total count of books taken and total count of
// days this user holds all the books.
// Returns: User details, Total books he/she is holding, Total days he/she holds them.
public class GetReport
{
    public static void MapEndpoint(WebApplication app) => app.MapGet("/api/report", 
        ([FromServices] GetReport handler) => handler.Execute()
    );

    public record ReportItemDto(string User, long[] Books, int TotalDays);
    private async Task<IResult> Execute()
    {
        var report = await _reportQuery.Execute();
        
        var reportDto = report.Select(r => new ReportItemDto(
            $"{r.User.FirstName} {r.User.LastName}",
            r.BookIds.ToArray(),
            r.TotalDays
        )).ToList();

        return Results.Json(reportDto);
    }

    private readonly BooksHoldingReportQuery _reportQuery;

    public GetReport(BooksHoldingReportQuery reportQuery)
    {
        _reportQuery = reportQuery;
    }
}