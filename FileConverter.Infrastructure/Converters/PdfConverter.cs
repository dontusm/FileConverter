using System.Text;
using System.Text.RegularExpressions;
using FileConverter.Domain.Abstractions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using FileConverter.Infrastructure.Constants;

namespace FileConverter.Infrastructure.Converters;

public class PdfConverter : IFileConverter
{
    public async Task<byte[]> ConvertToPdfAsync(Stream inputStream, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(inputStream, PdfConstants.FileEncoding);
        var text = await reader.ReadToEndAsync(cancellationToken);

        var lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);

        using var ms = new MemoryStream();
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont(PdfConstants.FontFamily, PdfConstants.FontSize, PdfConstants.FontStyle);

        var y = PdfConstants.Margin;

        foreach (var line in lines)
        {
            if (y + PdfConstants.LineHeight > page.Height - PdfConstants.Margin)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                y = PdfConstants.Margin;
            }

            gfx.DrawString(line, font, XBrushes.Black,
                new XRect(PdfConstants.Margin, y, page.Width - 2 * PdfConstants.Margin, page.Height - 2 * PdfConstants.Margin),
                XStringFormats.TopLeft);

            y += PdfConstants.LineHeight;
        }

        document.Save(ms);
        
        return ms.ToArray();
    }

    public async Task<string> ReadContentAsync(Stream pdfStream, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await pdfStream.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;

        var sb = new StringBuilder();
        using var document = UglyToad.PdfPig.PdfDocument.Open(memoryStream);

        foreach (var page in document.GetPages())
        {
            var lines = page.Text.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var cleanedLine = line
                    .Replace("\u0000", "")      
                    .Replace("\f", "")          
                    .Trim();

                if (!string.IsNullOrWhiteSpace(cleanedLine))
                {
                    sb.AppendLine(FixSpacing(cleanedLine));
                }
            }
        }

        return sb.ToString();
    }
    
    private static string FixSpacing(string input)
    {
        var withSpaces = Regex.Replace(input, @"(?<=[а-яa-z])(?=[А-ЯA-Z])", " ");
        
        withSpaces = Regex.Replace(withSpaces, @"\s{2,}", " ");

        return withSpaces.Trim();
    }
}