using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.IntegrationTests;

public class IntegrationTest
{
    protected readonly HttpClient Client;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbContextFactory<LibraryDbContext> _contextFactory;

    protected IntegrationTest()
    {
        var factory = new InMemoryWebApplicationFactory();

        _serviceProvider = factory.Services;
        Client = factory.CreateClient();
    }

    protected void GivenBooks(params Book[] books)
    {
        using var scope = _serviceProvider.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        
        dbContext.Books.AddRange(books);
        dbContext.SaveChanges();
    }
    
    protected void GivenUsers(params User[] users)
    {
        using var scope = _serviceProvider.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();
    }

    protected void GivenBooksTaken(params BooksTaken[] booksTaken)
    {
        using var scope = _serviceProvider.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        
        dbContext.BooksTaken.AddRange(booksTaken);
        dbContext.SaveChanges();
    }

    protected static async Task ShouldBeSuccess(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            // Just to see exception on the test result pane, makes alot easier to debug
            response.IsSuccessStatusCode.Should().BeTrue($"{response.StatusCode}: " + responseString);
        }
    }
}