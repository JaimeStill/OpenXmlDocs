namespace DocBuilder.Data.Entities
{
    public class DocT
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DocCategory? Category { get; set; }

        public ICollection<DocItemT> Items { get; set; } = new List<DocItemT>();

        public DocT(string name)
        {
            Name = name;
        }
    }
}