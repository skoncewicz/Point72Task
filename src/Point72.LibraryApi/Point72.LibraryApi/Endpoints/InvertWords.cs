namespace Point72.LibraryApi.Endpoints;

public class InvertWords
{
    public string Invert(string testString)
    {
        var separators = new[] { ' ', ',', ';', '!' };
        var words = testString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        
        var result = string.Join(' ', words.Reverse());
        return result;
    }
}