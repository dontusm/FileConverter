using FileConverter.Domain.Abstractions;

namespace FileConverter.Application.Interfaces;

public interface IFileConverterFactory
{
    IFileConverter GetConverter(string contentType);
}