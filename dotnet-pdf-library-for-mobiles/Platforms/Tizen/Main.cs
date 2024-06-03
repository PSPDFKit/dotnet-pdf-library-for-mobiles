using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace dotnet_pdf_library_for_mobiles;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
