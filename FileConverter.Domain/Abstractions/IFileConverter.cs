namespace FileConverter.Domain.Abstractions;

public interface IFileConverter
{ 
    public Task<byte[]> ConvertToPdfAsync(Stream inputStream, CancellationToken cancellationToken);
    
    public Task<string> ReadContentAsync(Stream inputStream, CancellationToken cancellationToken);
}