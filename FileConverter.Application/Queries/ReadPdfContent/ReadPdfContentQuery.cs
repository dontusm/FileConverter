using MediatR;

namespace FileConverter.Application.Queries.ReadPdfContent;

public record ReadPdfContentQuery(Stream FileStream, string ContentType) : IRequest<string>;