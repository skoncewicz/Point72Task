using FluentAssertions;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.UnitTests;

public class InvertWordsPreserveStructureTests
{
    private InvertWordsPreserveStructure _invertWords = new();
    
    [Test]
    public void ShouldInvertWordsSeparatedWithSpacesAndCommas()
    {
        var testString = "The quick brown fox, jumps over the lazy dog!";
        
        var result = _invertWords.Invert(testString);
        
        var expectedResult = "dog lazy the over, jumps fox brown quick The!";
        result.Should().Be(expectedResult);
    }

    [Test]
    public void ShouldReturnEmptyStringForEmptyString()
    {
        var result = _invertWords.Invert(string.Empty);

        result.Should().BeEmpty();
    }

    [Test]
    public void ShouldReturnWordForSingleWord()
    {
        var s = "word!";
        
        var result = _invertWords.Invert(s);

        result.Should().Be(s);
    }
    
    [Test]
    public void ShouldReportOriginalWhenOnlySeparators()
    {
        var s = ",;!;";
        
        var result = _invertWords.Invert(s);

        result.Should().Be(s);
    }
}