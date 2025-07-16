using FileConverter.Domain.Abstractions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using FileConverter.Infrastructure.Constants;

namespace FileConverter.Infrastructure.Converters;

public class PdfConverter : IFileConverter
{
    public async Task<byte[]> ConvertToPdfAsync(Stream inputStream)
    {
        using var reader = new StreamReader(inputStream, PdfConstants.FileEncoding);
        var text = await reader.ReadToEndAsync();

        var lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);

        using var ms = new MemoryStream();
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont(PdfConstants.FontFamily, PdfConstants.FontSize, PdfConstants.FontStyle);

        double y = PdfConstants.Margin;

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
}