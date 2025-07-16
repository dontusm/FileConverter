namespace FileConverter.Domain.Abstractions;

public interface IFileConverter
{
    Task<byte[]> ConvertToPdfAsync(Stream inputStream);
}