# PSPDFKit.NET for Mobiles (iOS & Android)

#### PSPDFKit

The [PSPDFKit SDK](https://pspdfkit.com/) is a framework that allows you to view, annotate, sign, and fill PDF forms on iOS, Android, Windows, macOS, and Web.

[PSPDFKit Instant](https://pspdfkit.com/instant) adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.

#### Related

- PSPDFKit.NET (Android): [PSPDFKit/dotnet-pdf-library-for-android](https://github.com/PSPDFKit/dotnet-pdf-library-for-android)
- PSPDFKit.NET (iOS): [PSPDFKit/dotnet-pdf-library-for-ios](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios)

## Support, Issues and License Questions

PSPDFKit offers support via https://pspdfkit.com/support/request/.

Are you evaluating our SDK? That's great, we're happy to help out!
To make sure this is fast, please use a work email and have someone from your company fill out our sales form: https://pspdfkit.com/sales/

## Requirements

#### Android requirements:

* **NET for Android workload >= 33.0.95/7.0.100**
* **Microsoft Mobile OpenJDK >= 11.0**
* Android **5** or newer / API level **21** or higher
* 32-bit or 64-bit ARM (armeabi-v7a with NEON / arm64-v8a) or 32-bit or 64-bit Intel x86 CPU.
* Projects using PSPDFKit.dotnet.Android.dll **must** set [Target Framework](https://developer.xamarin.com/guides/android/application_fundamentals/understanding_android_api_levels/#framework) to **API 33 (Android 13.0)** or higher.

#### iOS requirements:

- **.NET for iOS 17.2.8004/8.0.100 or higher +**
- **.NET for MacCatalyst 17.2.8004/8.0.100 or higher +**

## Integrating in a standard MAUI project

1. Create .NET MAUI project.
2. In **Platforms** directory, delete all platforms except **Android** and **iOS**.
3. To add iOS support, follow the instructions in [getting started guide for PSPDFKit.NET for iOS](https://pspdfkit.com/getting-started/dotnetformobile-ios)

> [!NOTE]  
> - Everything that goes in **Resources** directory in the getting started guide will go to **Resources/Raw** in MAUI project.
> - Most if not all the code, will go in files present in **Platforms/iOS** directory.

4. To add android support, follow the instructions in [getting started guide for PSPDFKit.NET for Android](https://pspdfkit.com/getting-started/dotnetformobile-android)

> [!NOTE]  
> - Everything that goes in **Resources** directory in the getting started guide will go to **Resources/Raw** in MAUI project.
> - Most if not all the code, will go in files present in **Platforms/Android** directory.
> - You do not need to set content view as this is handled by MAUI i.e. following code is not needed in MAUI:
> ```
> // Set your view from the "main" layout resource.
> SetContentView(Resource.Layout.activity_main);
> ```


### Contributing

Please ensure [you signed our CLA](https://pspdfkit.com/guides/web/current/miscellaneous/contributing/) so we can accept your contributions.