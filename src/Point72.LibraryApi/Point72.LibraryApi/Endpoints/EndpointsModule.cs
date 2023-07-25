namespace Point72.LibraryApi.Endpoints;

public static class EndpointsModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddTransient<GetBook>();
        services.AddTransient<GetInvertedBook>();
        services.AddTransient<GetReport>();
        services.AddTransient<InvertWords>();
        
        //services.AddTransient<IInvertWords, InvertWordsPreserveStructure>();
        services.AddTransient<IInvertWordsFast, InvertWordsPreserveStructureFast>();
    }
}