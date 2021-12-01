namespace DocBuilder.Data.Entities
{
    public class DocItemT
    {
        public int Id { get; set; }
        public int DocTId { get; set; }
        public DocItemType Type { get; set; }
        public int Index { get; set; }
        public string Value { get; set; }
        public bool AllowMultiple { get; set; }
        public bool IsDropdown { get; set; }

        public DocT? DocT { get; set; }

        public ICollection<DocOptionT> Options { get; set; } = new List<DocOptionT>();

        public DocItemT(string value, DocItemType type)
        {
            Value = value;
            Type = type;
        }
    }
}