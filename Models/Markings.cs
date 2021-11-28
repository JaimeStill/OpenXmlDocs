using DocumentFormat.OpenXml.Packaging;

namespace OpenXmlDocs.Models
{
    public record Markings
    {
        public FooterPart Foot { get; init; }
        public string FootId { get; init; }
        public HeaderPart Head { get; init; }
        public string HeadId { get; init; }

        public Markings(
            FooterPart foot,
            string footId,
            HeaderPart head,
            string headId
        )
        {
            this.Foot = foot;
            this.FootId = footId;
            this.Head = head;
            this.HeadId = headId;
        }
    }
}