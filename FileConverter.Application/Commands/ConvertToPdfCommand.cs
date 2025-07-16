using MediatR;

namespace FileConverter.Application.Commands;

public record ConvertToPdfCommand(Stream FileStream, string ContentType) : IRequest<byte[]>;