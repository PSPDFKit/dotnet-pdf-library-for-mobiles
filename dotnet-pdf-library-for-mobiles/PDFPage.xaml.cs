#if ANDROID
using dotnet_pdf_library_for_mobiles.Platforms.Android.Handlers;
#elif IOS
using dotnet_pdf_library_for_mobiles.Platforms.iOS.Handlers;
#endif

namespace dotnet_pdf_library_for_mobiles;

public partial class PDFPage : ContentPage
{
	public PDFPage()
	{
		InitializeComponent();
#if ANDROID
        Search.Handler = new SearchViewModularHandler();
        PdfView.Handler = new PdfViewHandler(((SearchViewModularHandler)Search.Handler).SearchViewModular);
#elif IOS
        PdfView.Handler = new PdfViewHandler();
#endif
    }

    private void OnSearchPressed(object sender, EventArgs e)
    {
#if ANDROID
        var modularSearchView = ((SearchViewModularHandler)Search.Handler).SearchViewModular;
        var pdfFragment = ((PdfViewHandler)PdfView.Handler).PdfFragment;

        if (modularSearchView.IsDisplayed)
            modularSearchView.Hide();
        else
            modularSearchView.SetDocument(pdfFragment.Document, pdfFragment.Configuration);
        modularSearchView.Show();
#endif
    }
}