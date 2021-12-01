namespace DocBuilder.Data.Entities
{
    public class TDocOption
    {
        public int Id { get; set; }
        public int TDocItemId { get; set; }
        public string Value { get; set; }

        public TDocItem? TDocItem { get; set; }

        public TDocOption(string value)
        {
            Value = value;
        }
    }
}