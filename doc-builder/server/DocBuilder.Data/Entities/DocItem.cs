namespace DocBuilder.Data.Entities
{
    public class DocItem
    {
        public int Id { get; set; }
        public int DocId { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }

        public Doc? Doc { get; set; }

        public DocItem(string type)
        {
            Type = type;
        }
    }
}