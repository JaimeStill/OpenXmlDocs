namespace DocBuilder.Data.Entities
{
    public class TDoc
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DocCategory? Category { get; set; }

        public ICollection<TDocItem> Items { get; set; } = new List<TDocItem>();

        public TDoc(string name)
        {
            Name = name;
        }
    }
}