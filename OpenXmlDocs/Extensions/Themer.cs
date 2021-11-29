using System;
using DocumentFormat.OpenXml.Wordprocessing;

namespace OpenXmlDocs.Extensions
{
    public static class Themer
    {
        public static void GenerateStyle(this Styles root, string styleId, Action<Style> config, StyleValues type = StyleValues.Paragraph)
        {
            var style = new Style()
            {
                Type = type,
                StyleId = styleId,
                CustomStyle = true
            };

            var name = new StyleName() { Val = styleId };
            style.AppendChild(name);

            config(style);

            root.Append(style);
        }

        public static void GenerateTheme(this Styles root)
        {
            root.GenerateHeadingStyle();
            root.GenerateMarkingStyle();
        }

        static void GenerateHeadingStyle(this Styles root) => GenerateStyle(
            root,
            "Heading1",
            style =>
            {
                var runProps = new StyleRunProperties(
                    new Bold(),
                    new FontSize() { Val = "36" },
                    new Color() { Val = "0090ff" },
                    new RunFonts { Ascii = "Franklin Gothic Demi", HighAnsi = "Franklin Gothic Demi" }
                );

                style.AppendChild(runProps);
            }
        );

        static void GenerateMarkingStyle(this Styles root) => GenerateStyle(
            root,
            "Marking",
            style =>
            {
                var runProps = new StyleRunProperties(
                    new Bold(),
                    new FontSize() { Val = "20" },
                    new Color() { Val = "00aa33" }
                );

                var paraProps = new StyleParagraphProperties(
                    new Justification() { Val = JustificationValues.Center },
                    new SpacingBetweenLines() { AfterAutoSpacing = true, BeforeAutoSpacing = true }
                );

                style.Append(runProps, paraProps);
            }
        );
    }
}