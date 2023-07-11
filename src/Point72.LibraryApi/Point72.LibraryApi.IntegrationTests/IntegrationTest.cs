using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.IntegrationTests;

public class IntegrationTest
{
    protected HttpClient Client;
    private WebApplicationFactory<Program> _factory;
    private LibraryDbContext _dbContext;
    private IServiceScope _scope;

    [SetUp]
    public void Setup()
    {
        _factory = new SqlLiteWebApplicationFactory();
        
        _scope = _factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureCreated();

        Client = _factory.CreateClient();
    }
    
    [TearDown]
    public void TearDown()
    {
        _dbContext.RemoveRange(_dbContext.Books);
        _dbContext.RemoveRange(_dbContext.Users);
        _dbContext.RemoveRange(_dbContext.Authors);
        _dbContext.RemoveRange(_dbContext.BooksTaken);

        _dbContext.SaveChanges();
    }

    protected void GivenBooks(params Book[] books)
    {
        _dbContext.Books.AddRange(books);
        _dbContext.SaveChanges();
    }
    
    protected void GivenUsers(params User[] users)
    {
        _dbContext.Users.AddRange(users);
        _dbContext.SaveChanges();
    }

    protected void GivenBooksTaken(params BooksTaken[] booksTaken)
    {
        _dbContext.BooksTaken.AddRange(booksTaken);
        _dbContext.SaveChanges();
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