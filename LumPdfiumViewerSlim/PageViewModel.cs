using Avalonia.Controls;
using Avalonia.Media;
using PdfiumViewer.Core;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                using var image = _pdfDocument.Render(page, 600, 800, 96, 96);
                            
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
                var jpegEncoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                    .First(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);

                var encoderParams = new System.Drawing.Imaging.EncoderParameters();
                encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                    System.Drawing.Imaging.Encoder.Quality, (long)70);

                image.Save(memoryStream, jpegEncoder, encoderParams);
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

        public void Dispose()
        {            
            _pdfDocument?.Dispose();
        }
    }
}
