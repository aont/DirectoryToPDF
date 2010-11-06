using System;
using System.Collections.Generic;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

namespace DirectoryToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                DirectoryInfo directory = new DirectoryInfo(arg);

                string SavePath = string.Format("{0}.pdf",directory.Name);

                
                var files = directory.GetFiles("*.bmp");

                using (PdfDocument Document = new PdfDocument())
                {
                    foreach (var file in files)
                    {
                        var filename = file.FullName;
                        Console.WriteLine(filename);
                        PdfPage page = Document.AddPage();
                        using (XImage image = XImage.FromFile(filename))
                        {

                            page.Width = image.PointWidth;
                            page.Height = image.PointHeight;
                            using (XGraphics gfx = XGraphics.FromPdfPage(page))
                            {
                                gfx.DrawImage(image, 0, 0);
                            }
                        }
                    }
                    Document.Save(SavePath);
                }
            }
        }
    }
}
