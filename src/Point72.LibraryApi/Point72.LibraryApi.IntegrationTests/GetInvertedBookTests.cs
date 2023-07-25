using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Point72.LibraryApi.Database;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.IntegrationTests;

public class GetInvertedBookTests : IntegrationTest
{
    [Test]
    public async Task ShouldReturnInvertedBook()
    {
        // arrange
        var author = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var book = new Book() { Id = 1, Title = "One two, three", Description = "Description", Author = author};
        GivenBooks(book);
        
        // act
        var response = await Client.GetAsync($"/api/invertwords/{book.Id}");
        
        await ShouldBeSuccess(response);
        var result = await response.Content.ReadFromJsonAsync<GetInvertedBook.BookDto>();

        
        // assert
        result.Should().NotBeNull();
        result.Title.Should().Be("three two, One");
        result.Description.Should().Be("Description");
    }
    
    [Test]
    public async Task ShouldReturnNotFoundWhenBookNotPresent()
    {
        // arrange no books
        
        // act
        var response = await Client.GetAsync($"/api/invertwords/42");
        
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}