namespace DocBuilder.Data.Entities
{
    public class TDocSelect : TDocItem
    {
        public string Value { get; set; }
        public bool AllowMultiple { get; set; }
        public bool IsDropdown { get; set; }

        public ICollection<TDocOption> Options { get; set; } = new List<TDocOption>();

        public TDocSelect(string type, string value) : base(type)
        {
            Value = value;
        }
    }
}