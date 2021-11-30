namespace DocBuilder.Data.Entities
{
    public class DocInfo : DocItem
    {
        public string Value { get; set; }

        public DocInfo(string type, string value) : base(type)
        {
            Value = value;
        }
    }
}