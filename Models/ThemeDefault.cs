using DocumentFormat.OpenXml.Wordprocessing;

namespace OpenXmlDocs.Models
{
    public record ThemeDefault
    {
        public ParagraphPropertiesDefault ParaDefault { get; init; }
        public RunPropertiesDefault RunDefault { get; init; }

        public ThemeDefault(
            ParagraphPropertiesDefault paraDefault,
            RunPropertiesDefault runDefault
        )
        {
            ParaDefault = paraDefault;
            RunDefault = runDefault;
        }
    }
}