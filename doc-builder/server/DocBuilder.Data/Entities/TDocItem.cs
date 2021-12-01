namespace DocBuilder.Data.Entities
{
    public class TDocItem
    {
        public int Id { get; set; }
        public int TDocId { get; set; }
        public DocItemType Type { get; set; }
        public int Index { get; set; }
        public string Value { get; set; }
        public bool AllowMultiple { get; set; }
        public bool IsDropdown { get; set; }

        public TDoc? TDoc { get; set; }

        public ICollection<TDocOption> Options { get; set; } = new List<TDocOption>();

        public TDocItem(string value, DocItemType type)
        {
            Value = value;
            Type = type;
        }
    }
}