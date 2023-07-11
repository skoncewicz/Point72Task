using FluentAssertions;

namespace Point72.LibraryApi.IntegrationTests;

public class ApplicationStartupTests : IntegrationTest
{
    [Test]
    public async Task RootPath_ReturnsOk()
    {
        var response = await Client.GetAsync("/healthcheck");
        
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Be("OK");
    }
}