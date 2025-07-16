using FileConverter.Application.Interfaces;
using MediatR;

namespace FileConverter.Application.Queries.ReadPdfContent;

public class ReadPdfContentHandler : IRequestHandler<ReadPdfContentQuery, string>
{
    private readonly IFileConverterFactory _factory;

    public ReadPdfContentHandler(IFileConverterFactory factory)
    {
        _factory = factory;
    }

    public Task<string> Handle(ReadPdfContentQuery request, CancellationToken cancellationToken)
    {
        var converter = _factory.GetConverter(request.ContentType);
        return converter.ReadContentAsync(request.FileStream, cancellationToken);
    }
}