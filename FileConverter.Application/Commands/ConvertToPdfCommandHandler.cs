using FileConverter.Application.Interfaces;
using MediatR;

namespace FileConverter.Application.Commands;

public class ConvertToPdfHandler : IRequestHandler<ConvertToPdfCommand, byte[]>
{
    private readonly IFileConverterFactory _factory;

    public ConvertToPdfHandler(IFileConverterFactory factory)
    {
        _factory = factory;
    }

    public Task<byte[]> Handle(ConvertToPdfCommand request, CancellationToken cancellationToken)
    {
        var converter = _factory.GetConverter(request.ContentType);
        return converter.ConvertToPdfAsync(request.FileStream);
    }
}