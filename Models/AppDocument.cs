using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace OpenXmlDocs.Models
{
    public record AppDocument
    {
        public Body Body { get; init; }
        public MainDocumentPart Main { get; init; }

        public AppDocument(
            Body body,
            MainDocumentPart main
        )
        {
            Body = body;
            Main = main;
        }

        public void Append(IEnumerable<OpenXmlElement> elements) => Body.Append(elements);
        public void Append(params OpenXmlElement[] elements) => Body.Append(elements);

        [return: NotNullIfNotNull("element")]
        public T? AppendChild<T>(T? element) where T : OpenXmlElement => Body.AppendChild(element);
    }
}