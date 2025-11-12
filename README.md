# LumPdfiumViewerSlim

LumPdfiumViewerSlim is a streamlined version of PdfiumViewer. The core of PdfiumViewer has been separated, and the WPF components have been removed. It has been re-implemented using AvaloniaUI and supports AOT (Ahead-Of-Time) compilation to enable fast file preview.

<img width="402" height="332" alt="image" src="https://github.com/user-attachments/assets/5fd90471-2090-44ce-ae2b-3ab06f640a89" />

*GDI is used in System.Drawing to support print function, which is no longer officially supported on Linux / macOS.*

## Key Features

- **WPF Removal**: The original WPF elements, including bookmarks and scroll views, have been removed.
- **AvaloniaUI Implementation**: Pages are rendered and displayed in the simplest way through virtual mode.
- **AOT Support**: AOT compilation is supported for fast startup.
- **Single-File Compression**: Despite AOT's fast startup, we prefer single-file compressed releases. A single-file compressed package, including unmanaged libraries, is only 25.9 MB in size.

## License

LumPdfiumViewerSlim is released under the [Apache 2.0 license](https://www.apache.org/licenses/LICENSE-2.0).
