using FileConverter.Application.Interfaces;
using FileConverter.Infrastructure.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<PdfConverter>();
        services.AddScoped<IFileConverterFactory, FileConverterFactory>();
        
        return services;
    }
}