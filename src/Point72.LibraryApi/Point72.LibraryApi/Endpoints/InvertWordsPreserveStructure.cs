using System.Text;

namespace Point72.LibraryApi.Endpoints;

public class InvertWordsPreserveStructure : IInvertWords
{
    public string Invert(string testString)
    {
        var separators = new[] { ' ', ',', ';', '!' };
        var words = testString.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var result = new StringBuilder();
        int wordIndex = 0;
        
        for(int i = 0; i < testString.Length; i++)
        {
            var c = testString[i];
            if (separators.Contains(c))
            {
                result.Append(c);
            }
            else
            {
                var replacingWordIndex = words.Length - wordIndex - 1;
                result.Append(words[replacingWordIndex]);
                i += words[wordIndex].Length - 1;
                
                wordIndex++;
            }
        }
        return result.ToString();
    }
}