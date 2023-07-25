namespace Point72.LibraryApi.Endpoints;

/// <summary>
/// Leaving this as a separate class, so it can be rewritten to use 0-allocation.
/// But I'm going on vacation today so no time :(
/// </summary>
public class InvertWords : IInvertWords
{
    public string Invert(string testString)
    {
        var separators = new[] { ' ', ',', ';', '!' };
        var words = testString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        
        var result = string.Join(' ', words.Reverse());
        return result;
    }
}