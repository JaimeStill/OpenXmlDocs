namespace DocBuilder.Data.Entities
{
    public class DocItem
    {
        public int Id { get; set; }
        public int DocId { get; set; }
        public DocItemType Type { get; set; }
        public int Index { get; set; }
        public string Value { get; set; }
        public bool AllowMultiple { get; set; }
        public bool IsDropdown { get; set; }

        public DocAnswer? Answer { get; set; }
        public Doc? Doc { get; set; }

        public ICollection<DocOption> Options { get; set; } = new List<DocOption>();

        public DocItem(string value, DocItemType type)
        {
            Value = value;
            Type = type;
        }
    }
}