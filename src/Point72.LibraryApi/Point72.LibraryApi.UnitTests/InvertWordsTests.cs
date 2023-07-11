using FluentAssertions;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.UnitTests;

public class InvertWordsTests
{
    [Test]
    public void ShouldInvertWordsSeparatedWithSpacesAndCommas()
    {
        var testString = "The quick brown fox, jumps over the lazy dog!";

        var invertWords = new InvertWords();
        var result = invertWords.Invert(testString);
        
        var expectedResult = "dog lazy the over jumps fox brown quick The";
        result.Should().Be(expectedResult);
    }

    [Test]
    public void ShouldReturnEmptyStringForEmptyString()
    {
        var result = new InvertWords().Invert(string.Empty);

        result.Should().BeEmpty();
    }

    [Test]
    public void ShouldReturnWordForSingleWord()
    {
        var result = new InvertWords().Invert("word");

        result.Should().Be("word");
    }
}