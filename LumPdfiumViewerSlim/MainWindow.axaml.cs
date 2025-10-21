using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PdfiumViewer.Core;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {

        public PageViewModel RenderedPages { get; } = new PageViewModel();

        public MainWindow()
        {
            InitializeComponent();
            RenderedPages?.Dispose();
            DataContext= this;
        }     

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filters = new()
                {
                    new FileDialogFilter { Name = "PDF Files", Extensions = new() { "pdf" } },
                    //new FileDialogFilter { Name = "All Files", Extensions = new() { "*" } }
                }
            };

            var files = await openFileDialog.ShowAsync(this);
            if (files?.Length > 0)
            {
                RenderedPages.Load(files?[0] ?? string.Empty);
            }
        }
               

       
    }
}