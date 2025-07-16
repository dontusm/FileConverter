using FileConverter.Application.Commands;
using FileConverter.Application.Queries.ReadPdfContent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileConverter.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("convert-to-pdf")]
    public async Task<IActionResult> ConvertToPdf(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        
        var pdfBytes = await _mediator.Send(new ConvertToPdfCommand(stream, file.ContentType));
        
        return File(pdfBytes, "application/pdf", "converted.pdf");
    }
}