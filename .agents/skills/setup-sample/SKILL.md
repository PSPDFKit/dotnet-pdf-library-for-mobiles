---
name: setup-sample
description: Help users set up and run the .NET MAUI cross-platform sample project. Use when asked to run the sample, set up the project, or get started with Nutrient .NET for iOS and Android via MAUI.
---

# Set Up and Run the MAUI Cross-Platform Sample Project

Guides the user through setting up and running the `dotnet-pdf-library-for-mobiles` .NET MAUI sample project, which demonstrates Nutrient PDF viewing on Android, iOS, and Mac Catalyst from a single codebase.

## Important Rules

- **Always check prerequisites first** before attempting to build.
- **Stop on errors** and help the user diagnose before proceeding.
- **macOS is required** for iOS and Mac Catalyst targets. Android can be built on any OS.
- This project uses **NuGet packages directly** -- no binding project build is required.

## Workflow

### Step 1: Check Prerequisites

Verify the development environment is ready:

```bash
# Check .NET SDK is installed
dotnet --version

# Check MAUI workload is installed
dotnet workload list | grep maui
```

**Required:**
- .NET SDK 9.0+
- .NET MAUI workload

If the MAUI workload is missing:
```bash
dotnet workload install maui
```

**Platform-specific requirements:**

For **Android**:
```bash
dotnet workload list | grep android
java -version
adb devices  # if running on emulator/device
```

For **iOS** (macOS only):
```bash
dotnet workload list | grep ios
xcode-select -p
```

For **Mac Catalyst** (macOS only):
```bash
dotnet workload list | grep maccatalyst
```

### Step 2: Choose Target Platform

Ask the user which platform they want to run on:
- **Android** - Emulator or connected device
- **iOS** - Simulator or connected device (macOS only)
- **Mac Catalyst** - Native macOS app (macOS only)

### Step 3: Restore and Build

From the `dotnet-pdf-library-for-mobiles/` directory:

**For Android:**
```bash
cd dotnet-pdf-library-for-mobiles
dotnet restore
dotnet build -f net10.0-android
```

**For iOS:**
```bash
cd dotnet-pdf-library-for-mobiles
dotnet restore
dotnet build -f net10.0-ios
```

**For Mac Catalyst:**
```bash
cd dotnet-pdf-library-for-mobiles
dotnet restore
dotnet build -f net10.0-maccatalyst
```

### Step 4: Run the App

**Android (emulator or device):**
```bash
cd dotnet-pdf-library-for-mobiles
dotnet build -f net10.0-android -t:Install
```
Or if the Install target fails (APK can be ~135MB):
```bash
adb install -r bin/Debug/net10.0-android/*-Signed.apk
adb shell am start -n com.companyname.dotnetpdflibraryformobiles/crc64*/dotnet_pdf_library_for_mobiles.MainActivity
```

**iOS Simulator:**
```bash
cd dotnet-pdf-library-for-mobiles
dotnet build -f net10.0-ios -t:Run
```

**Mac Catalyst:**
```bash
cd dotnet-pdf-library-for-mobiles
dotnet build -f net10.0-maccatalyst -t:Run
```

### Step 5: Configure License Key

The project uses trial mode by default. If the user has a license key, it needs to be set per platform:

**Android** - In `Platforms/Android/MainApplication.cs` or `Platforms/Android/MainActivity.cs`:
```csharp
PSPDFKit.NutrientGlobal.Initialize(this, licenseKey: "YOUR_LICENSE_KEY");
```

**iOS** - In `Platforms/iOS/AppDelegate.cs`:
```csharp
PSPDFKit.Model.PSPDFKitGlobal.SetLicenseKey("YOUR_LICENSE_KEY");
```

**Mac Catalyst** - In `Platforms/MacCatalyst/AppDelegate.cs`:
```csharp
PSPDFKit.Model.PSPDFKitGlobal.SetLicenseKey("YOUR_LICENSE_KEY");
```

License keys are available from https://my.nutrient.io/.

### Step 6: Verify the App Runs

The app should launch with a flyout navigation menu containing:
- **Playground** - Interactive PDF viewing and testing
- **File Picker Example** - Open PDF documents from the device
- **About** - App information

Test opening a document through the file picker to confirm PDF rendering works.

If the app crashes on Android, check logs:
```bash
adb logcat -d | grep -iE "AndroidRuntime|MonoDroid|UNHANDLED" | tail -20
```

## Troubleshooting

### Build fails with missing workload
```bash
dotnet workload install maui
```
This installs MAUI and all platform workloads (Android, iOS, Mac Catalyst).

### NuGet package restore fails
Verify nuget.org is configured:
```bash
dotnet nuget list source
```

### Android build fails with "Type X is defined multiple times"
Clean and rebuild:
```bash
cd dotnet-pdf-library-for-mobiles
rm -rf bin obj
dotnet restore
dotnet build -f net10.0-android
```

### iOS build fails on non-macOS
iOS and Mac Catalyst targets can only be built on macOS with Xcode installed.

### Large APK size on Android
This is expected (~135MB). The Nutrient SDK includes native libraries for multiple ABIs. For production, use `<AndroidSupportedAbis>` in the `.csproj` to limit architectures.

## Key Files

| File | Purpose |
|------|---------|
| `dotnet-pdf-library-for-mobiles/dotnet-pdf-library-for-mobiles.csproj` | Multi-target MAUI project with NuGet references |
| `dotnet-pdf-library-for-mobiles/MauiProgram.cs` | MAUI app initialization and handler registration |
| `dotnet-pdf-library-for-mobiles/Platforms/Android/MainActivity.cs` | Android entry point |
| `dotnet-pdf-library-for-mobiles/Platforms/iOS/AppDelegate.cs` | iOS entry point |
| `dotnet-pdf-library-for-mobiles/Platforms/MacCatalyst/AppDelegate.cs` | Mac Catalyst entry point |
| `dotnet-pdf-library-for-mobiles/Views/PdfView.cs` | Cross-platform PDF viewer control |
| `ReadMe.md` | Full integration documentation |
