namespace DocBuilder.Data.Entities
{
    public class DocQuestion : DocItem
    {
        public string Value { get; set; }

        public DocAnswer? Answer { get; set; }

        public DocQuestion(string type, string value) : base(type)
        {
            Value = value;
        }
    }
}