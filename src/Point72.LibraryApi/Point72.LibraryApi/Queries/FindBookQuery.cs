﻿using Microsoft.EntityFrameworkCore;
using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.Queries;

public class FindBookQuery
{
    private readonly LibraryDbContext _libraryDbContext;

    public FindBookQuery(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    public async Task<Book?> ExecuteAsync(long id)
    {
        var book = await _libraryDbContext.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        return book;
    }
}