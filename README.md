# LumPdfiumViewerSlim

LumPdfiumViewerSlim is a streamlined version of PdfiumViewer. The core of PdfiumViewer has been separated, and the WPF components have been removed. It has been re-implemented using AvaloniaUI and supports AOT (Ahead-Of-Time) compilation to enable fast file preview.

<img width="377" height="329" alt="image" src="https://github.com/user-attachments/assets/f0e141f5-746b-4b10-b192-3e38523c22e9" />


## Key Features

- **WPF Removal**: The original WPF elements, including bookmarks and scroll views, have been removed.
- **AvaloniaUI Implementation**: Pages are rendered and displayed in the simplest way through virtual mode.
- **AOT Support**: AOT compilation is supported for fast startup.
- **Single-File Compression**: Despite AOT's fast startup, we prefer single-file compressed releases. A single-file compressed package, including unmanaged libraries, is only 25.9 MB in size.

## License

LumPdfiumViewerSlim is released under the [Apache 2.0 license](https://www.apache.org/licenses/LICENSE-2.0).
