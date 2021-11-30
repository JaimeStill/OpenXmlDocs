namespace DocBuilder.Data.Entities
{
    public class Doc
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DocCategory? Category { get; set; }

        public ICollection<DocItem> Items { get; set; } = new List<DocItem>();

        public Doc(string name)
        {
            Name = name;
        }
    }
}