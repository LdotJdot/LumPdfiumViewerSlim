using Avalonia.Controls;
using Avalonia.Media;
using PdfiumViewer.Core;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    public interface IPageRender
    {
        public int page { get; }

        public IImage Image {get;}
    }

    public class PageRender
    {
        PdfDocument _pdfDocument;
        public PageRender(PdfDocument _pdfDocument, int pageNumber)
        {
            this._pdfDocument= _pdfDocument;
            this.page = pageNumber;
        }

        public IImage Image=>GetImage();

       IImage GetImage()
        {
            try
            {
                var size = _pdfDocument.PageSizes[page];
                using var image = _pdfDocument.Render(page, (int)size.Width , (int)size.Height, 128, 128);
                            
                return ConvertToAvaloniaBitmap(image);
            }
            catch
            {
                return null;
            }
        }


        private Avalonia.Media.Imaging.Bitmap ConvertToAvaloniaBitmap(System.Drawing.Image image)
        {
            using (var memoryStream = new MemoryStream())
            {                
                image.Save(memoryStream,ImageFormat.Png);
                memoryStream.Position = 0;
                return new Avalonia.Media.Imaging.Bitmap(memoryStream);
            }
        }

        public int page { get; }
    }

    public class PageViewModel: ReactiveObject,IDisposable
    {
        private PdfDocument _pdfDocument;

        IEnumerable<PageRender> _displayedData=[];

        public IEnumerable<PageRender> DisplayedData
        {
            get => _displayedData;
            private set => this.RaiseAndSetIfChanged(ref _displayedData, value);
        }

        public void Load(string path)
        {
            _pdfDocument?.Dispose();
            _pdfDocument = PdfDocument.Load(path);
            Initialize(_pdfDocument);
        }

        private void Initialize(PdfDocument pdfDocument)
        {
            _pdfDocument = pdfDocument;

            // 页面范围
            DisplayedData = Enumerable.Range(0, pdfDocument.PageCount).Select(o=>new PageRender(_pdfDocument, o));

        }

        public void Print()
        {
            using var pd = _pdfDocument.CreatePrintDocument();

            var ps = new PrinterSettings();
            ps.PrinterName = "Microsoft Print to PDF";
            ps.Copies = 1;

            // 4. 页面设置（纸张/边距）
            var pgs = new PageSettings(ps)
            {
                Margins = new Margins(0, 0, 0, 0)
            };
            foreach (PaperSize sz in ps.PaperSizes)
            {
                if (sz.PaperName.Equals("A4", StringComparison.OrdinalIgnoreCase))
                {
                    pgs.PaperSize = sz;
                    break;
                }
            }

            // 5. 应用设置并静默打印
            pd.PrinterSettings = ps;
            pd.DefaultPageSettings = pgs;
            pd.PrintController = new StandardPrintController(); // 不弹进度框
            pd.Print();
        }

        public void Dispose()
        {            
            _pdfDocument?.Dispose();
        }
    }
}
