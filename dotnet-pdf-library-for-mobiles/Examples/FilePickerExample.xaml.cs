namespace dotnet_pdf_library_for_mobiles.Examples;

public partial class FilePickerExample : ContentPage
{
        public FilePickerExample()
        {
                InitializeComponent();
        }

        private async void OnPickFileClicked(object sender, EventArgs e)
        {
                var customFileType = new FilePickerFileType(
                        new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                                { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                                { DevicePlatform.MacCatalyst, new[] { "com.adobe.pdf" } },
                                { DevicePlatform.Android, new[] { "application/pdf" } },
                        });

                var options = new PickOptions
                {
                        PickerTitle = "Select a PDF file",
                        FileTypes = customFileType,
                };

                var result = await FilePicker.Default.PickAsync(options);

                if (result != null)
                {
                        pdfView.LoadDocument(result.FullPath);
                }
        }
}
