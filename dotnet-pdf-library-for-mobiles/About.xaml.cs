namespace dotnet_pdf_library_for_mobiles;

public partial class About : ContentPage
{
    public About()
    {
        InitializeComponent();
        Loaded += async (s,e) => {
            await using var fileStream = await FileSystem.Current.OpenAppPackageFileAsync("license.txt");
            using var reader = new StreamReader(fileStream);
            LicenseText.Text = await reader.ReadToEndAsync();
        };
    }
}

