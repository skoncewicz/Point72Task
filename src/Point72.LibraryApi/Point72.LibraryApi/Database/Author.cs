namespace Point72.LibraryApi.Database;

public class Author
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<Book> Books { get; set; } = new List<Book>();
}