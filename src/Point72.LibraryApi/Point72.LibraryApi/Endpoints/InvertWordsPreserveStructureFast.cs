namespace Point72.LibraryApi.Endpoints;

public class InvertWordsPreserveStructureFast : IInvertWordsFast
{
    private static bool IsSeparator(char c)
        => c is ' ' or ',' or ';' or '!' or '-';

    public string Invert(ReadOnlySpan<char> testString)
    {
        Span<char> resultChars = stackalloc char[testString.Length];
        
        int reversedWordStart = testString.Length - 1;

        int writePtr = 0;

        for (int readPtr = 0; readPtr < testString.Length; readPtr++)
        {
            if (IsSeparator(testString[readPtr])) // just copy when separator sign
            {
                resultChars[writePtr++] = testString[readPtr];
            }
            else
            {
                // find the start of the next reversed word in line
                while (!IsWordStart(reversedWordStart, testString))
                    reversedWordStart--;

                // copy reversed word to result
                for (int i = reversedWordStart; i == 0 || !IsWordEnd(i - 1, testString); i++)
                    resultChars[writePtr++] = testString[i];

                reversedWordStart--;

                // fast forward until the end of current words
                while (!IsWordEnd(readPtr, testString))
                    readPtr++;
            }
        }
        return new string(resultChars);
    }

    private static bool IsWordStart(int pos, ReadOnlySpan<char> s) =>
        !IsSeparator(s[pos]) && (pos == 0 || IsSeparator(s[pos - 1]));
    
    private static bool IsWordEnd(int pos, ReadOnlySpan<char> s) =>
        !IsSeparator(s[pos]) && (pos == s.Length - 1 || IsSeparator(s[pos + 1]));
}