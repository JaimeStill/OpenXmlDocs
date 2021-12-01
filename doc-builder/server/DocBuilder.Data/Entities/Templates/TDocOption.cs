namespace DocBuilder.Data.Entities
{
    public class DocOptionT
    {
        public int Id { get; set; }
        public int DocItemTId { get; set; }
        public string Value { get; set; }

        public DocItemT? DocItemT { get; set; }

        public DocOptionT(string value)
        {
            Value = value;
        }
    }
}