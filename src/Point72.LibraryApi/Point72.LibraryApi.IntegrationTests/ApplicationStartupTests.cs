using FluentAssertions;

namespace Point72.LibraryApi.IntegrationTests;

public class ApplicationStartupTests : IntegrationTest
{
    [Test]
    public async Task RootPath_ReturnsOk()
    {
        var response = await Client.GetAsync("/");
        
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Be("Application started!");
    }
}