# Learning - OpenXML Word Processing

* [Overview](#overview)
* [Running and Debugging](#running-and-debugging)
    * [OpenXmlDocs](#openxmldocs)
* [Research Notes](./notes.md)  
    * [API Documentation Epiphany](./notes.md#api-documentation-epiphany)
    * [Default and Defined Styles](./notes.md#default-and-defined-styles)
    * [Image Metadata](./notes.md#image-metadata)

![example](./assets/example-light.png#gh-light-mode-only)
![example](./assets/example-dark.png#gh-dark-mode-only)

## Overview

Developing an understanding of the [OpenXML API](https://docs.microsoft.com/en-us/office/open-xml/working-with-wordprocessingml-documents) ([API Repository](https://github.com/OfficeDev/Open-XML-SDK)), particularly related to automating Wordprocessing documents.

The intent here is to be able to develop data structures and an API that facilitates the creation of document templates stored in a database. These templates can then be used to generate document instances (also stored in a database) that can be rendered and completed via a web interface. The data structures generated on the client as JSON data can then be sent to the server and passed into the API to generate OpenXML-based documents.

The end state being that we can begin to digitize the formal data centered around workflows and staffing processes within our organization. Not only will this free the data from being locked behind programatically difficult (sometimes impossible) to read binary files, it allows get away from reliance on the proprietary and expensive PDF format.

> Beginning to iron out some patterns and naming standards for abstracting repeatable behaviors that make working with the SDK way quicker.

## Running and Debugging

### OpenXmlDocs

This application is a simple .NET 6 console application. As long as you have the [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed, you will be able to run the application by calling `dotnet build` from the terminal in VS Code. You can optionally pass in an argument that provides a name for the `.docx` file generated in `Environment.CurrentDirectory`:

```bash
# generates demo.docx in project root
dotnet run -- demo
```

You can also debug with <kbd>f5</kbd> and step through the code in VS Code so long as you have the [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) installed. [launch.json](./.vscode/launch.json) is configured to pass in the name *test* as the name of the generated `.docx` file.