namespace DocBuilder.Data.Entities
{
    public class TDocInfo : TDocItem
    {
        public string Value { get; set; }

        public TDocInfo(string type, string value) : base(type)
        {
            Value = value;
        }
    }
}