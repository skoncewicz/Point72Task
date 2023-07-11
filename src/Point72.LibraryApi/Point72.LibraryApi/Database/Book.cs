namespace Point72.LibraryApi.Database;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = String.Empty; 
    public string? Description { get; set; }

    // Navigation Properties
    public Author Author { get; set; } = new ();
    public ICollection<BooksTaken> BooksTaken { get; set; }
}