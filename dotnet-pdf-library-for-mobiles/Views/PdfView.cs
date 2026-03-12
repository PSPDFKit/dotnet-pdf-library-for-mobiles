namespace dotnet_pdf_library_for_mobiles.Views;

public class PdfView : ContentView
{
    public void LoadDocument(string filePath)
    {
        var handler = this.Handler as IPdfViewHandler;
        handler?.LoadDocumentFromPath(filePath);
    }

    public void LoadDocumentFromAssets(string assetName)
    {
        var handler = this.Handler as IPdfViewHandler;
        handler?.LoadDocumentFromAssets(assetName);
    }
}
