# OpenXmlDocs

## Overview

This is the application that I built in order to learn how to build Wordprocessing documents in the OpenXML SDK. Additionally, it taught me how to start abstracting some of the standard capabilities of the SDK in a way that makes development faster and more effective.

## Running and Debugging

This application is a simple .NET 6 console application. As long as you have the [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed, you will be able to run the application by calling `dotnet build` from the terminal in VS Code. You can optionally pass in an argument that provides a name for the `.docx` file generated in `Environment.CurrentDirectory`:

```bash
# generates demo.docx in project root
dotnet run -- demo
```

You can also debug with <kbd>f5</kbd> and step through the code in VS Code so long as you have the [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) installed. [launch.json](./.vscode/launch.json) is configured to pass in the name *test* as the name of the generated `.docx` file.