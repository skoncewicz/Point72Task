namespace Point72.LibraryApi.Queries;

public static class QueriesModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddTransient<EnsureDbConnectionQuery>();
        services.AddTransient<SearchBooksQuery>();
        services.AddTransient<FindBookQuery>();
    }
}