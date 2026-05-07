# Nutrient .NET for Mobiles (MAUI)

Agent instructions for the `dotnet-pdf-library-for-mobiles` repository. This is a .NET MAUI cross-platform sample app demonstrating Nutrient PDF viewing on Android, iOS, and Mac Catalyst.

## Architecture Overview

This is a **single-project MAUI application** that uses Nutrient NuGet packages to provide PDF viewing across three platforms from one codebase. It serves as a reference implementation for integrating Nutrient into a MAUI project.

### How It Works

1. A cross-platform `PdfView` control defines the platform-agnostic API.
2. Platform-specific `PdfViewHandler` implementations map `PdfView` to native Nutrient viewers.
3. MAUI's Handler pattern connects the cross-platform control to the native implementation at runtime.
4. NuGet packages provide the Nutrient SDK -- no binding project build is required.

Read the `Views/` directory for the current control interface and `Platforms/*/Handlers/` for the native implementations.

### Directory Structure

```text
combined/
├── dotnet-pdf-library-for-mobiles/
│   ├── *.csproj                       # Multi-target MAUI project
│   ├── MauiProgram.cs                 # App config, handler registration
│   ├── Views/                         # Cross-platform PDF control and handler interface
│   ├── Platforms/
│   │   ├── Android/                   # Android-specific code (SDK init, handlers, listeners)
│   │   ├── iOS/                       # iOS-specific code (handlers)
│   │   └── MacCatalyst/               # MacCatalyst-specific code (handlers)
│   ├── Examples/                      # Example XAML pages demonstrating SDK usage
│   └── Resources/                     # App resources (PDFs, icons, fonts, styles)
│
├── ReadMe.md
└── LICENSE.md
```

When working in this project, list directory contents to discover the current files — specific pages, handlers, and listeners may change across versions.

### Handler Pattern

The core architectural pattern is MAUI's Handler system. Read `MauiProgram.cs` to see how handlers are registered, and `Views/` to see the cross-platform control interface.

The correct platform-specific handler is selected at compile time via conditional imports. Each platform handler in `Platforms/*/Handlers/` wraps the native Nutrient viewer in a MAUI-compatible view.

Read the handler files directly to understand the current implementation for each platform.

### NuGet Dependencies

Platform-conditional package references are defined in the `.csproj` using `<Choose>/<When>` blocks conditioned on `$(TargetFramework)`. Read the `.csproj` to discover the current packages and their versions for each platform.

### Navigation Structure

The app uses a flyout-based navigation pattern. Read the main page XAML files to discover the current navigation structure and available example pages.

### SDK Initialization

SDK initialization is platform-specific. Read `Platforms/Android/MainActivity.cs` for Android init and check iOS/MacCatalyst entry points for their initialization approach.

### Key Build Commands

```bash
# Restore and build for a specific platform
cd dotnet-pdf-library-for-mobiles
dotnet restore
dotnet build -f net10.0-android      # or net10.0-ios or net10.0-maccatalyst

# Run on Android emulator/device
dotnet build -f net10.0-android -t:Install

# Run on iOS Simulator
dotnet build -f net10.0-ios -t:Run

# Run on Mac Catalyst
dotnet build -f net10.0-maccatalyst -t:Run
```

### Target Frameworks and Minimum Versions

Read the `.csproj` file for the current target frameworks and minimum OS versions — these change across .NET and SDK releases.

### Version Management

NuGet package versions in the `.csproj` are updated during the release process by the `bin/update_combined.sh` script. Android and iOS versions are updated independently since they follow different release cadences.

### Related Repositories

- [PSPDFKit/dotnet-pdf-library-for-android](https://github.com/PSPDFKit/dotnet-pdf-library-for-android) -- Android binding project (produces `Nutrient.dotnet.Android` NuGet)
- [PSPDFKit/dotnet-pdf-library-for-ios](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios) -- iOS binding project (produces iOS + MacCatalyst NuGet packages)
