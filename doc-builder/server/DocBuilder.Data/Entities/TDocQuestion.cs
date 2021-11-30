namespace DocBuilder.Data.Entities
{
    public class TDocQuestion : TDocItem
    {
        public string Value { get; set; }

        public TDocQuestion(string type, string value) : base(type)
        {
            Value = value;
        }
    }
}