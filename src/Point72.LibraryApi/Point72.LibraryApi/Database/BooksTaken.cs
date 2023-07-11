namespace Point72.LibraryApi.Database;

public class BooksTaken
{
    public DateTime DateTaken { get; set; }

    // Navigation Properties
    public Book Book { get; set; } = new();
    public User User { get; set; } = new();
}