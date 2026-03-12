namespace dotnet_pdf_library_for_mobiles.Views;

public interface IPdfViewHandler
{
    Task LoadDocumentFromPath(string filePath);
    Task LoadDocumentFromAssets(string assetName);
}
