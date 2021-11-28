# Learning - OpenXML Word Processing

* [Overview](#overview)
* [Running and Debugging](#running-and-debugging)
* [Research Notes](./notes.md)  
    * [API Documentation Epiphany](./notes.md#api-documentation-epiphany)
    * [Default and Defined Styles](./notes.md#default-and-defined-styles)
    * [Image Metadata](./notes.md#image-metadata)

![example](./assets/example-light.png#gh-light-mode-only)
![example](./assets/example-dark.png#gh-dark-mode-only)

## Overview

The intent behind this repository is to capture the required details necessary to automate the process of generating Word documents.

> Beginning to iron out some patterns and naming standards for abstracting repeatable behaviors that make working with the SDK way quicker.

## Running and Debugging

This application is a simple .NET 6 console application. As long as you have the [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed, you will be able to run the application by calling `dotnet build` from the terminal in VS Code. You can optionally pass in an argument that provides a name for the `.docx` file generated in `Environment.CurrentDirectory`:

```bash
# generates demo.docx in project root
dotnet run -- demo
```

You can also debug with <kbd>f5</kbd> and step through the code in VS Code so long as you have the [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) installed. [launch.json](./.vscode/launch.json) is configured to pass in the name *test* as the name of the generated `.docx` file.