using System.Net.Http.Json;
using FluentAssertions;
using Point72.LibraryApi.Database;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.IntegrationTests;

public class GetReportTests : IntegrationTest
{
    [Test]
    public async Task ShouldReturnReportsForAllUsers()
    {
        // arrange
        var today = DateTime.Now;
        
        var user1 = new User { Id = 1, FirstName = "John", LastName = "Doe" };
        var user2 = new User { Id = 2, FirstName = "Jane", LastName = "Doe" };
        GivenUsers(user1, user2);
        
        var book1 = new Book { Id = 1, Title = "Book 1", Description = "Description 1" };
        var book2 = new Book { Id = 2, Title = "Book 2", Description = "Description 2" };
        var book3 = new Book { Id = 3, Title = "Book 3", Description = "Description 3" };
        GivenBooks(book1, book2, book3);

        var booksTaken = new BooksTaken[]
        {
            new() {Book = book1, User = user1, DateTaken = today.AddDays(-1) },
            new() {Book = book2, User = user2, DateTaken = today.AddDays(-2) },
            new() {Book = book3, User = user2, DateTaken = today.AddDays(-3) },
        };
        GivenBooksTaken(booksTaken);
        
        // act
        var response = await Client.GetAsync("/api/report");
        await ShouldBeSuccess(response);
        var reportItems = await response.Content.ReadFromJsonAsync<List<GetReport.ReportItemDto>>();

        // assert
        var expectedReportItems = new[]
        {
            new GetReport.ReportItemDto("John Doe", new[] { book1.Id }, 1),
            new GetReport.ReportItemDto("Jane Doe", new[] { book2.Id, book3.Id }, 5)
        };

        reportItems.Should().BeEquivalentTo(expectedReportItems);
    }
}