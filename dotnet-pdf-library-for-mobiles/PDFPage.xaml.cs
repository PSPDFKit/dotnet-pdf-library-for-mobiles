namespace dotnet_pdf_library_for_mobiles;

public partial class PDFPage : ContentPage
{
	public PDFPage()
	{
		InitializeComponent();
        LoadDocumentButton.Command = PdfView.LoadDocumentCommand;
    }
}