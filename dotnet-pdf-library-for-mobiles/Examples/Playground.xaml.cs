namespace dotnet_pdf_library_for_mobiles.Examples;

public partial class Playground : ContentPage
{
        public Playground()
        {
                InitializeComponent();
                Loaded += Playground_Loaded;
        }

        private void Playground_Loaded(object? sender, EventArgs e)
        {
                pdfView.LoadDocumentFromAssets("document.pdf");
        }
}
