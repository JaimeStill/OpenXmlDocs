namespace DocBuilder.Data.Entities
{
    public class DocOption
    {
        public int Id { get; set; }
        public int DocItemId { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        public DocItem? DocItem { get; set; }

        public DocOption(string value)
        {
            Value = value;
        }
    }
}