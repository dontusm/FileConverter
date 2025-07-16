using FileConverter.Application.Interfaces;
using FileConverter.Domain.Abstractions;
using FileConverter.Infrastructure.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Infrastructure;

public class FileConverterFactory : IFileConverterFactory
{
    private readonly IServiceProvider _provider;

    public FileConverterFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IFileConverter GetConverter(string contentType)
    {
        return contentType switch
        {
            "text/plain" => _provider.GetRequiredService<PdfConverter>(),
            _ => throw new NotSupportedException($"Unsupported content type: {contentType}")
        };
    }
}