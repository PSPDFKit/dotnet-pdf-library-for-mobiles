using System.Windows.Input;

namespace dotnet_pdf_library_for_mobiles;

public partial class PdfView : ContentView
{
    public event EventHandler? LoadDocument;

    private ICommand? _loadDocumentCommand;

    public ICommand LoadDocumentCommand
        => _loadDocumentCommand ??= new Command(() => LoadDocument?.Invoke(this, EventArgs.Empty));

    public PdfView()
	{
		InitializeComponent();
	}
}