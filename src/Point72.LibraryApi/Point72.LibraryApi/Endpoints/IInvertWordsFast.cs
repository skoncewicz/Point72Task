namespace Point72.LibraryApi.Endpoints;

public interface IInvertWordsFast
{
    string Invert(ReadOnlySpan<char> testString);
}