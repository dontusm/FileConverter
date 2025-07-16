using System.Text;
using PdfSharpCore.Drawing;

namespace FileConverter.Infrastructure.Constants;

public class PdfConstants
{
    public const string FontFamily = "Arial";
    
    public const double FontSize = 12;
    
    public const double Margin = 20;
    
    public const double LineHeight = 18;
    
    public const XFontStyle FontStyle = XFontStyle.Regular;
    
    public static readonly Encoding FileEncoding = Encoding.UTF8;
}