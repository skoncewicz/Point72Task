using Microsoft.EntityFrameworkCore;

namespace Point72.LibraryApi.Database;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BooksTaken> BooksTaken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTableNames(modelBuilder);

        ConfigureBook(modelBuilder);
        ConfigureBooksTaken(modelBuilder);
        
        ConfigureBooksTakenManyToMany(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureTableNames(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("User");

        modelBuilder.Entity<Author>()
            .ToTable("Author");

        modelBuilder.Entity<Book>()
            .ToTable("Book");

        modelBuilder.Entity<BooksTaken>()
            .ToTable("BooksTaken");
    }

    private static void ConfigureBooksTakenManyToMany(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BooksTaken>()
            .HasOne(bt => bt.Book)
            .WithMany(b => b.BooksTaken)
            .HasForeignKey("BookID");

        modelBuilder.Entity<BooksTaken>()
            .HasOne(bt => bt.User)
            .WithMany(u => u.BooksTaken)
            .HasForeignKey("UserID");
    }

    private static void ConfigureBook(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey("AuthorID");
    }

    private static void ConfigureBooksTaken(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BooksTaken>()
            .Property<long>("BookID");

        modelBuilder.Entity<BooksTaken>()
            .Property<long>("UserID");

        modelBuilder.Entity<BooksTaken>()
            .HasKey("BookID", "UserID");
    }
}