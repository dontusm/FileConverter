namespace FileConverter.Api;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument(config =>
        {
            config.Title = "FileConverter API";
            config.Version = "v1";
        });
        
        return services;
    }
}