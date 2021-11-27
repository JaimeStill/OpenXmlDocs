using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ImageMagick;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace OpenXmlDocs
{
    class Program
    {
        static void Main(string[] args)
        {
            var title = GenerateTitle(args);
            var path = Path.Join(Environment.CurrentDirectory, title);
            if (File.Exists(path)) File.Delete(path);

            var res = CreateDocument(path);
            if (res) Console.WriteLine("Document successfully created!");

            res = ModifyDocument(path);
            if (res) Console.WriteLine("Document successfully modified!");

            res = BuildDocumentTable(path);
            if (res) Console.WriteLine("Document table successfully added!");
        }

        static string GenerateTitle(string[] args)
        {
            var title = args.Length > 0 && !string.IsNullOrEmpty(args[0])
                ? args[0]
                : PromptTitle();

            return title.ToLower().EndsWith(".docx")
                ? title
                : $"{title}.docx";
        }

        static bool CreateDocument(string path)
        {

            using var file = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
            var main = file.AddMainDocumentPart();
            InitDefaultStyles(main);

            main.Document = new Document();
            var body = main.Document.AppendChild(new Body());

            CreateHeaderImage(main, body, "Generative Documents");

            var para = body.AppendChild(new Paragraph());
            var run = para.AppendChild(new Run());
            var text = new Text
            {
                Text = "This was generated automatically! ",
                Space = SpaceProcessingModeValues.Preserve
            };

            run.AppendChild(text);

            return true;
        }

        static bool ModifyDocument(string path)
        {
            using var file = WordprocessingDocument.Open(path, true);
            var body = file.MainDocumentPart?.Document.Body;

            if (body is not null)
            {
                var para = body.AppendChild(new Paragraph());
                var run = para.AppendChild(new Run());

                run.Append(
                    new RunProperties(new Italic(), new Bold()),
                    new Text("This document has been modified programmatically!")
                );

                return true;
            }
            else return false;
        }

        static bool BuildDocumentTable(string path)
        {
            using var file = WordprocessingDocument.Open(path, true);
            var body = file.MainDocumentPart?.Document.Body;

            if (body is not null)
            {
                var tbl = new Table();
                var tblProps = new TableProperties();
                var tblStyle = new TableStyle { Val = "TableGrid" };

                // make the table width 100% of the page width.
                var tblWidth = new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct };

                tblProps.Append(tblStyle, tblWidth);
                tbl.AppendChild(tblProps);

                var tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn());
                tbl.AppendChild(tg);

                var tr1 = new TableRow();

                var tc1 = new TableCell(new Paragraph(new Run(new Text("Justified Left"))));

                var tc2 = new TableCell(
                    new Paragraph(
                        new ParagraphProperties(new Justification
                        {
                            Val = JustificationValues.Center
                        }),
                        new Run(new Text("Justified Center"))
                    )
                );

                var tc3 = new TableCell(
                    new Paragraph(
                        new ParagraphProperties(new Justification
                        {
                            Val = JustificationValues.End
                        }),
                        new Run(new Text("Justified Right"))
                    )
                );

                tr1.Append(tc1, tc2, tc3);

                tbl.AppendChild(tr1);
                body.AppendChild(tbl);

                return true;
            }
            else return false;
        }

        static string PromptTitle()
        {
            Console.WriteLine("What would you like to name this document?");
            var title = Console.ReadLine();

            while (string.IsNullOrEmpty(title))
                title = Console.ReadLine();

            return title;
        }

        static void InitDefaultStyles(MainDocumentPart main)
        {
            var part = main.AddNewPart<StyleDefinitionsPart>();
            var root = new Styles();

            var runDefaults = new RunPropertiesDefault(
                new RunPropertiesBaseStyle(
                    new RunFonts()
                    {
                        Ascii = "Tenorite",
                        HighAnsi = "Tenorite"
                    },
                    new FontSize() { Val = "24" }
                )
            );

            var paraDefaults = new ParagraphPropertiesDefault(
                new ParagraphPropertiesBaseStyle(
                    new SpacingBetweenLines() { AfterAutoSpacing = true, BeforeAutoSpacing = true }
                )
            );

            root.DocDefaults = new DocDefaults();

            root.DocDefaults.Append(runDefaults, paraDefaults);

            GenerateHeaderStyle(root);

            part.Styles = root;
        }

        static void CreateHeaderImage(MainDocumentPart main, Body body, string text)
        {
            var image = InitImage(main, body);
            var tbl = new Table(
                new TableProperties(
                    new TableWidth() { Width = "3333", Type = TableWidthUnitValues.Pct },
                    new TableBorders(
                        new TopBorder() { Val = BorderValues.Single, Size = 1 },
                        new RightBorder() { Val = BorderValues.Single, Size = 1 },
                        new BottomBorder() { Val = BorderValues.Single, Size = 1 },
                        new LeftBorder() { Val = BorderValues.Single, Size = 1 }
                    )
                )
            );

            var tg = new TableGrid(new GridColumn(), new GridColumn());
            tbl.AppendChild(tg);

            var tr = new TableRow(
                new TableRowProperties(
                    new TableRowHeight() { HeightType = HeightRuleValues.AtLeast, Val = 2250 }
                ),
                new TableCell(
                    new TableCellProperties(
                        new Justification() { Val = JustificationValues.Center },
                        new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center }
                    ),
                    new Paragraph(
                        new ParagraphProperties(
                            new Justification() { Val = JustificationValues.Center }
                        ),
                        new Run(image)
                    )
                ),
                new TableCell(
                    new TableCellProperties(
                        new Justification() { Val = JustificationValues.Center },
                        new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center }
                    ),
                    new Paragraph(
                        new ParagraphProperties(
                            new ParagraphStyleId() { Val = "Heading1" },
                            new Justification() { Val = JustificationValues.Center }
                        ),
                        new Run(new Text(text))
                    )
                )
            );

            tbl.AppendChild(tr);

            body.AppendChild(tbl);
        }

        static void CreateHeader(Body body)
        {
            var header = body.AppendChild(new Paragraph());
            header.Append(
                new ParagraphProperties(
                    new ParagraphStyleId() { Val = "Heading1" }
                ),
                new Run(
                    new Text("Generative Documents")
                )
            );
        }

        static void CreateImage(MainDocumentPart main, Body body)
        {
            var image = InitImage(main, body);

            body.AppendChild(new Paragraph(
                new Run(image)
            ));
        }

        static void GenerateHeaderStyle(Styles styles)
        {
            var h1Style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = "Heading1",
                CustomStyle = true
            };

            var h1Name = new StyleName() { Val = "Heading1" };

            var h1RunProps = new StyleRunProperties(
                new Bold(),
                new FontSize() { Val = "36" },
                new Color() { Val = "0090ff" },
                new RunFonts { Ascii = "Franklin Gothic Demi", HighAnsi = "Franklin Gothic Demi" }
            );

            h1Style.Append(h1Name, h1RunProps);

            styles.Append(h1Style);
        }

        static Drawing InitImage(MainDocumentPart main, Body body)
        {
            var imagePath = Path.Join(
                Environment.CurrentDirectory,
                "assets",
                "atom.png"
            );

            var imagePart = LoadImageIntoDoc(main, imagePath);
            var imageSize = CalcImageSize(imagePath);

            var image = EmbedImage(
                main.GetIdOfPart(imagePart),
                "atom.png",
                imageSize.width,
                imageSize.height
            );

            return image;
        }

        static ImagePart LoadImageIntoDoc(MainDocumentPart main, string path)
        {
            var imagePart = main.AddImagePart(ImagePartType.Png);
            using var stream = new FileStream(path, FileMode.Open);
            imagePart.FeedData(stream);

            return imagePart;
        }

        static (long width, long height) CalcImageSize(string path)
        {
            const int maxSize = 128;
            const int emusPerInch = 9525;

            var info = new MagickImageInfo(path);
            float width = info.Width;
            float height = info.Height;
            var outOfBounds = (info.Width > maxSize) || (info.Height > maxSize);

            if (outOfBounds)
            {
                if (width > height)
                {
                    var ratio = height / width;
                    width = maxSize;
                    height = width * ratio;
                }
                else
                {
                    var ratio = width / height;
                    height = maxSize;
                    width = height * ratio;
                }
            }

            height *= emusPerInch;
            width *= emusPerInch;

            return (Convert.ToInt64(width), Convert.ToInt64(height));
        }

        static Drawing EmbedImage(string embed, string name, long width, long height)
        {
            var element = new Drawing(
                new DW.Inline(
                    new DW.Extent() { Cx = width, Cy = height },
                    new DW.EffectExtent()
                    {
                        LeftEdge = 0L,
                        TopEdge = 0L,
                        RightEdge = 0L,
                        BottomEdge = 0L
                    },
                    new DW.DocProperties()
                    {
                        Id = (UInt32Value)1U,
                        Name = name
                    },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                        new A.GraphicFrameLocks() { NoChangeAspect = true }
                    ),
                    new A.Graphic(
                        new A.GraphicData(
                            new PIC.Picture(
                                new PIC.NonVisualPictureProperties(
                                    new PIC.NonVisualDrawingProperties()
                                    {
                                        Id = (UInt32Value)0U,
                                        Name = name
                                    },
                                    new PIC.NonVisualPictureDrawingProperties()
                                ),
                                new PIC.BlipFill(
                                    new A.Blip(
                                        new A.BlipExtensionList(
                                            new A.BlipExtension()
                                            {
                                                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                            }
                                        )
                                    )
                                    {
                                        Embed = embed,
                                        CompressionState = A.BlipCompressionValues.None
                                    },
                                    new A.Stretch(new A.FillRectangle())
                                ),
                                new PIC.ShapeProperties(
                                    new A.Transform2D(
                                        new A.Offset() { X = 0L, Y = 0L },
                                        new A.Extents() { Cx = width, Cy = height }
                                    ),
                                    new A.PresetGeometry(
                                        new A.AdjustValueList()
                                    )
                                    {
                                        Preset = A.ShapeTypeValues.Rectangle
                                    }
                                )
                            )
                        )
                        {
                            Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                        }
                    )
                )
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U,
                    EditId = "50D07946",

                }
            );

            return element;
        }
    }
}