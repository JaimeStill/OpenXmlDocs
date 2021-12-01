namespace DocBuilder.Data.Entities
{
    public class DocAnswer
    {
        public int Id { get; set; }
        public int DocItemId { get; set; }
        public string Value { get; set; }

        public DocItem? DocItem { get; set; }

        public DocAnswer(string value)
        {
            Value = value;
        }
    }
}