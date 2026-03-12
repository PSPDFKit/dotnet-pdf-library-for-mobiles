namespace dotnet_pdf_library_for_mobiles;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainFlyoutPage());
	}
}
