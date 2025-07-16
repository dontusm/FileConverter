namespace FileConverter.Domain.Abstractions;

public interface IFileConverter
{ 
    public Task<byte[]> ConvertToPdfAsync(Stream inputStream, CancellationToken cancellationToken);
}