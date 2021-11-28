using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlDocs.Models;

namespace OpenXmlDocs.Extensions
{
    public static class Builder
    {
        public static Table GenerateImageCard(
            MainDocumentPart main,
            string imageUrl,
            params string[] details
        )
        {
            if (details.Length < 1) throw new ArgumentException("GenerateImageCard: params string[] details is empty");

            var justify = JustificationValues.End;

            var image = main.InitImage(imageUrl);

            var tbl = new Table(
                new TableProperties(
                    new Justification() { Val = JustificationValues.Center },
                    new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct },
                    GenerateTableBorders()
                ),
                new TableGrid(new GridColumn(), new GridColumn())
            );

            var row = new TableRow();

            row.AppendChild(new TableCell(
                new TableCellProperties(
                    new Justification() { Val = JustificationValues.Center },
                    new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center },
                    new TableCellWidth() { Width = "Auto" },
                    GenerateTableCellMargin("180")
                ),
                GenerateParagraph(image, () =>
                    new ParagraphProperties(
                        new Justification() { Val = JustificationValues.Center },
                        GenerateAutoSpacing()
                    )
                )
            ));

            var cell = new TableCell(
                new TableCellProperties(
                    new Justification() { Val = justify },
                    new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center },
                    GenerateTableCellMargin("180")
                )
            );

            foreach ((string value, int i) in details.Select((value, i) => (value, i)))
                cell.AppendChild(
                    GenerateParagraph(new Text(value), () =>
                    {
                        return i > 0
                            ? new ParagraphProperties(
                                new Justification() { Val = justify },
                                GenerateAutoSpacing()
                              )
                            : new ParagraphProperties(
                                new Justification() { Val = justify },
                                new ParagraphStyleId() { Val = "Heading1" },
                                GenerateAutoSpacing()
                              );
                    })
                );
            
            row.AppendChild(cell);
            tbl.AppendChild(row);

            return tbl;
        }

        public static Markings GenerateMarkings(MainDocumentPart main, Text headText, Text footText)
        {
            var head = GenerateHeadMarkings(main, headText);
            var foot = GenerateFootMarkings(main, footText);

            return new Markings(foot, main.GetIdOfPart(foot), head, main.GetIdOfPart(head));
        }

        public static Paragraph GenerateParagraph(OpenXmlElement element, Func<ParagraphProperties>? paraStyle = null, Func<RunProperties>? runStyle = null)
        {
            var para = new Paragraph();
            if (paraStyle is not null)
                para.AppendChild(paraStyle());

            var run = new Run();

            if (runStyle is not null)
                run.AppendChild(runStyle());

            run.AppendChild(element);
            para.AppendChild(run);

            return para;
        }

        public static SpacingBetweenLines GenerateAutoSpacing() =>
            new SpacingBetweenLines() { AfterAutoSpacing = true, BeforeAutoSpacing = true };

        public static PageMargin GeneratePageMargin(uint margin, uint run) =>
            new PageMargin()
            {
                Footer = run, Header = run,
                Top = (int)margin, Right = margin,
                Bottom = (int)margin, Left = margin
            };

        public static TableBorders GenerateTableBorders(uint size = 1, BorderValues val = BorderValues.Single) =>
            new TableBorders(
                new TopBorder() { Val = val, Size = size },
                new RightBorder() { Val = val, Size = size },
                new BottomBorder() { Val = val, Size = size },
                new LeftBorder() { Val = val, Size = size }
            );

        public static TableCellMargin GenerateTableCellMargin(string width) =>
            new TableCellMargin(
                new TopMargin() { Width = width },
                new RightMargin() { Width = width },
                new BottomMargin() { Width = width },
                new LeftMargin() { Width = width }
            );

        static FooterPart GenerateFootMarkings(MainDocumentPart main, Text text)
        {
            var foot = main.AddNewPart<FooterPart>();

            var footer = new Footer(
                GenerateParagraph(text, () =>
                    new ParagraphProperties(
                        new ParagraphStyleId() { Val = "Marking" }
                    )
                )
            );

            foot.Footer = footer;
            
            return foot;
        }

        static HeaderPart GenerateHeadMarkings(MainDocumentPart main, Text text)
        {
            var head = main.AddNewPart<HeaderPart>();
            string headId = main.GetIdOfPart(head);

            var header = new Header(
                GenerateParagraph(text, () =>
                    new ParagraphProperties(
                        new ParagraphStyleId() { Val = "Marking" }
                    )
                )
            );

            head.Header = header;
            
            return head;
        }
    }
}