namespace DocBuilder.Data.Entities
{
    public class DocSelect : DocItem
    {
        public string Value { get; set; }
        public bool AllowMultiple { get; set; }
        public bool IsDropdown { get; set; }

        public ICollection<DocOption> Options { get; set; } = new List<DocOption>();

        public DocSelect(string type, string value) : base(type)
        {
            Value = value;
        }
    }
}