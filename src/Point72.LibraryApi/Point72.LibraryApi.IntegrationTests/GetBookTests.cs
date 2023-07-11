using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Point72.LibraryApi.Database;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.IntegrationTests;

public class GetBookTests : IntegrationTest
{
    [Test]
    public async Task ShouldReturnAllBooks()
    {
        // arrange
        GivenBooks(
            new Book { Title = "Book 1" }
        );
        
        // act
        var books = await ActSearchBooks("");
        
        // assert
        books.Should().HaveCount(1);
        books.Single().Title.Should().Be("Book 1");
    }

    [Test]
    public async Task ShouldSearchByAuthors()
    {
        // arrange
        GivenBooks(
            new Book { Title = "Book 1", Author = new Author { FirstName = "John", LastName = "Doe" } },
            new Book { Title = "Book 2", Author = new Author { FirstName = "Alice", LastName = "John" } },
            new Book { Title = "Book 3", Author = new Author { FirstName = "Barrack", MiddleName = "John", LastName = "Trump" } },
            new Book { Title = "Book 4", Author = new Author { FirstName = "Peter", LastName = "Watts" } }
        );
        
        // act
        var books = await ActSearchBooks("author=john");

        // assert
        books.Should().HaveCount(3);
        books.Select(b => b.Title).Should().BeEquivalentTo(
            "Book 1", "Book 2", "Book 3"
        );
    }
    
    [Test]
    public async Task ShouldSearchByText()
    {
        // arrange
        GivenBooks(
            new Book { Title = "Book about wolves", Description = "This is a book about John Doe" },
            new Book { Title = "Another book about wolves", Description = null },
            new Book { Title = "Animals of the forest", Description = "Wolves, bears, and other animals."},
            new Book { Title = "States of Matter", Description = "Now it is our turn to study statistical mechanics."}
        );
        
        // act
        var books = await ActSearchBooks("text=wolves");

        // assert
        books.Should().HaveCount(3);
        books.Select(b => b.Title).Should().BeEquivalentTo(
            "Book about wolves", "Another book about wolves", "Animals of the forest"
        );
    }
    
    [Test]
    public async Task ShouldSearchByUser()
    {
        // arrange
        var book1 = new Book { Title = "Book 1" };
        var book2 = new Book { Title = "Book 2" };
        GivenBooks(book1, book2);

        var user = new User { FirstName = "Zbigniew" };
        GivenUsers(user);
        
        var bookTaken = new BooksTaken{ Book = book1, User = user };
        GivenBooksTaken(bookTaken);
        
        // act
        var books = await ActSearchBooks($"userId={user.Id}");
        
        // assert
        books.Should().OnlyContain(
            b => b.Title == "Book 1"
        );
    }

    [Test]
    public async Task ShouldCombineUsingAndOperator()
    {
        // arrange
        GivenBooks(
            new Book { Title = "Book 1", Author = new Author { FirstName = "John", LastName = "Doe" } },
            new Book { Title = "Book Awesome 2", Author = new Author { FirstName = "Alice", LastName = "John" } },
            new Book { Title = "Book Awesome 3", Author = new Author { FirstName = "Barrack", MiddleName = "John", LastName = "Trump" } },
            new Book { Title = "Book 4", Author = new Author { FirstName = "Peter", LastName = "Watts" } }
        );
        
        // act
        var books = await ActSearchBooks("text=awesome&author=john");
        
        // assert
        books.Select(b => b.Title).Should().BeEquivalentTo(
            "Book Awesome 2", "Book Awesome 3"
        );
    }
    
    private async Task<List<GetBook.BookDto>> ActSearchBooks(string query)
    {
        var response = await Client.GetAsync($"/api/search?{query}");
        
        await ShouldBeSuccess(response);
        
        var result = await response.Content.ReadFromJsonAsync<List<GetBook.BookDto>>();
        result.Should().NotBeNull();
        
        return result!;
    }

    [Test]
    public async Task ShouldThrowWhenUnsupportedParameter()
    {
        // act
        var response = await Client.GetAsync("/api/search?unsupported=1");
        
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}