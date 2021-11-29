# Learning - OpenXML Word Processing

* [Overview](#overview)
* Projects
    * [OpenXmlDocs](./OpenXmlDocs/readme.md)
    * [doc-builder](./doc-builder/readme.md)
* [Research Notes](./notes.md)  
    * [API Documentation Epiphany](./notes.md#api-documentation-epiphany)
    * [Default and Defined Styles](./notes.md#default-and-defined-styles)
    * [Image Metadata](./notes.md#image-metadata)

![example](./assets/example-light.png#gh-light-mode-only)
![example](./assets/example-dark.png#gh-dark-mode-only)

## Status

Applying the knowledge gained from building out the [OpenXmlDocs](./OpenXmlDocs) project into a fullstack [.NET 6 / Angular 13 app](./doc-builder) that accomplishes my higher vision of automating document generation for web-based documents stored in a SQL database.

## Overview

Developing an understanding of the [OpenXML API](https://docs.microsoft.com/en-us/office/open-xml/working-with-wordprocessingml-documents) ([API Repository](https://github.com/OfficeDev/Open-XML-SDK)), particularly related to automating Wordprocessing documents.

The intent here is to be able to develop data structures and an API that facilitates the creation of document templates stored in a database. These templates can then be used to generate document instances (also stored in a database) that can be rendered and completed via a web interface. The data structures generated on the client as JSON data can then be sent to the server and passed into the API to generate OpenXML-based documents.

The end state being that we can begin to digitize the formal data centered around workflows and staffing processes within our organization. Not only will this free the data from being locked behind programatically difficult (sometimes impossible) to read binary files, it allows get away from reliance on the proprietary and expensive PDF format.