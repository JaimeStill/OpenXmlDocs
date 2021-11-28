using System;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlDocs.Models;

namespace OpenXmlDocs.Extensions
{
    public static class App
    {
        public static string GetAssetFile(string file) => Path.Join(
            Environment.CurrentDirectory,
            "assets",
            file
        );

        public static AppDocument InitDocument(this WordprocessingDocument file, bool create = true)
        {
            if (create)
            {
                var main = file.AddMainDocumentPart()
                               .InitStyles(styles => styles.GenerateTheme());

                main.Document = new Document();
                var body = main.Document.AppendChild(new Body());

                return new AppDocument(body, main);
            }
            else
            {
                if (file.MainDocumentPart is not null && file.MainDocumentPart.Document.Body is not null)
                    return new AppDocument(file.MainDocumentPart.Document.Body, file.MainDocumentPart);
                else
                    throw new FormatException("The provided document is incorrectly formatted");
            }
        }

        public static void InitMarkings(this Body body, Markings markings) =>
            body.PrependChild(new SectionProperties(
                new HeaderReference() { Id = markings.HeadId },
                new FooterReference() { Id = markings.FootId },
                Builder.GeneratePageMargin(740, 140)
            ));

        public static string InitPath(string title)
        {
            var path = Path.Join(Environment.CurrentDirectory, title);
            if (File.Exists(path)) File.Delete(path);

            return path;
        }
        public static MainDocumentPart InitStyles(
            this MainDocumentPart main,
            Action<Styles> theme
        )
        {
            var part = main.AddNewPart<StyleDefinitionsPart>();
            var root = new Styles();
            root.DocDefaults = new DocDefaults();

            var defaults = InitThemeDefaults();

            root.DocDefaults.Append(
                defaults.RunDefault,
                defaults.ParaDefault
            );

            theme(root);

            part.Styles = root;

            return main;
        }

        public static ThemeDefault InitThemeDefaults()
        {
            var runDefaults = new RunPropertiesDefault(
                new RunPropertiesBaseStyle(
                    new RunFonts()
                    {
                        Ascii = "Arial Nova",
                        HighAnsi = "Arial Nova"
                    },
                    new FontSize() { Val = "24" }
                )
            );

            var paraDefaults = new ParagraphPropertiesDefault(
                new ParagraphPropertiesBaseStyle(
                    new SpacingBetweenLines() { After = "20", Before = "20", AfterLines = 60, BeforeLines = 60 }
                )
            );

            return new ThemeDefault(paraDefaults, runDefaults);
        }

        public static string InitTitle(this string[] args)
        {
            var title = args.Length > 0 && !string.IsNullOrEmpty(args[0])
                ? args[0]
                : PromptTitle();

            return title.ToLower().EndsWith(".docx")
                ? title
                : $"{title}.docx";
        }

        static string PromptTitle()
        {
            Console.WriteLine("What would you like to name this document?");
            var title = Console.ReadLine();

            while (string.IsNullOrEmpty(title))
                title = Console.ReadLine();

            return title;
        }
    }
}