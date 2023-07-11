namespace Point72.LibraryApi.Database;

public class User
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<BooksTaken> BooksTaken { get; set; } = new List<BooksTaken>();
}