namespace DocBuilder.Data.Entities
{
    public class DocCategory
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public ICollection<Doc> Docs { get; set; } = new List<Doc>();
        public ICollection<TDoc> TDocs { get; set; } = new List<TDoc>();

        public DocCategory(string value)
        {
            Value = value;
        }
    }
}