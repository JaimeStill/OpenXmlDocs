namespace DocBuilder.Data.Entities
{
    public class TDocItem
    {
        public int Id { get; set; }
        public int TDocId { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }

        public TDoc? TDoc { get; set; }

        public TDocItem(string type)
        {
            Type = type;
        }
    }
}