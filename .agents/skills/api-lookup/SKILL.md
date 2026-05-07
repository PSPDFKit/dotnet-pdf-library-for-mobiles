---
name: api-lookup
description: Look up Nutrient API documentation for Android or iOS. Use when the user asks about a Nutrient API, class, method, annotation type, configuration option, or how to use a specific SDK feature in this MAUI project.
---

# Nutrient API Lookup (Cross-Platform)

Helps users find and understand Nutrient SDK APIs for this MAUI project by consulting the official documentation for both Android and iOS.

## Workflow

### Step 1: Determine the Target Platform

This MAUI project targets multiple platforms. Determine which platform the user is asking about:

- **Android-specific code** (in `Platforms/Android/`) -> use Android API docs
- **iOS/MacCatalyst-specific code** (in `Platforms/iOS/` or `Platforms/MacCatalyst/`) -> use iOS API docs
- **Cross-platform question** -> look up both and explain the differences

### Step 2: Fetch the API Index

Fetch the relevant llms.txt file:

**For Android:**
```
WebFetch: https://www.nutrient.io/api/android/llms.txt
```

**For iOS:**
```
WebFetch: https://www.nutrient.io/api/ios/llms.txt
```

Search the response for packages, classes, or components matching the user's query. The index will show the current SDK structure, available packages/frameworks, and their classes.

### Step 3: Fetch Specific API Documentation

Once you identify the relevant class or package from the llms.txt index, follow the links provided there to fetch the full documentation page. If the llms.txt entry provides a direct URL, use it. Otherwise, construct the URL using the base path and the class/package structure as shown in the index.

### Step 4: Translate to MAUI Context

When providing answers, write C# code that works within this MAUI project's architecture:

1. **Read the project's `.csproj`** to discover the current NuGet package references and target frameworks.
2. **Read the platform handler files** under `Platforms/*/Handlers/` to understand how native APIs are currently used in this project.
3. **Read `Views/PdfView.cs` and `IPdfViewHandler.cs`** to understand the cross-platform control interface.
4. Apply standard binding conventions:
   - For Android: Java `camelCase` -> C# `PascalCase`, Java packages -> C# namespaces.
   - For iOS: Objective-C types typically keep their names in C#.

### Step 5: Show Integration Example

When showing code examples, read the existing handler and view files to understand the current MAUI Handler pattern, then demonstrate how the new API fits into the same architecture:

1. **Cross-platform layer** — the `PdfView` control and `IPdfViewHandler` interface.
2. **Platform handlers** — the platform-specific handler implementations.
3. **XAML pages** — the example pages that use the control.

## Notes

- Android and iOS have different API surfaces — a feature available on one platform may not exist or work differently on the other.
- This project uses NuGet packages (not local binding projects), so the full native API is available without modification.
- For guides and tutorials: `https://www.nutrient.io/guides/android/` and `https://www.nutrient.io/guides/ios/`
