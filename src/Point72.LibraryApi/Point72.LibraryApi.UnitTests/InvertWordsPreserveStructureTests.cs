using FluentAssertions;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.UnitTests;

public class InvertWordsPreserveStructureTests
{
    private InvertWordsPreserveStructure _invertWords = new();
    private InvertWordsPreserveStructureFast _invertWordsOptimized = new();

    [Theory]
    public void ShouldInvertWordsSeparatedWithSpacesAndCommas(bool useFast)
    {
        var testString = "The quick brown fox, jumps over the lazy dog!";
        
        var result = Act(testString, useFast);
        
        var expectedResult = "dog lazy the over, jumps fox brown quick The!";
        result.Should().Be(expectedResult);
    }

    [Theory]
    public void ShouldReturnEmptyStringForEmptyString(bool useFast)
    {
        var result = Act(string.Empty, useFast);

        result.Should().BeEmpty();
    }

    [Theory]
    public void ShouldReturnWordForSingleWord(bool useFast)
    {
        var s = "word!";
        
        var result = Act(s, useFast);

        result.Should().Be(s);
    }
    
    [Theory]
    public void ShouldReportOriginalWhenOnlySeparators(bool useFast)
    {
        var s = ",;!;";

        var result = Act(s, useFast);

        result.Should().Be(s);
    }

    [Theory]
    public void ShouldWorkWithSingleLetterWords(bool useFast)
    {
        var testString = "a, two, c, d";
        
        var result = Act(testString, useFast);
        
        var expectedResult = "d, c, two, a";
        result.Should().Be(expectedResult);
    }
    
    [Theory]
    public void ShouldWorkWhenStringStartsWithSeparator(bool useFast)
    {
        var testString = "! oh dog";
        
        var result = Act(testString, useFast);
        
        var expectedResult = "! dog oh";
        result.Should().Be(expectedResult);
    }
    
    [Theory]
    public void ShouldWorkWhenStringEndsWithSeparator(bool useFast)
    {
        var testString = "oh dog !";
        
        var result = Act(testString, useFast);
        
        var expectedResult = "dog oh !";
        result.Should().Be(expectedResult);
    }

    private string Act(string s, bool useFast)
    {
        if(useFast)
            return _invertWordsOptimized.Invert(s);
        
        return _invertWords.Invert(s);
    }
}