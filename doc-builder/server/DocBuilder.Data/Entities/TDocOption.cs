namespace DocBuilder.Data.Entities
{
    public class TDocOption
    {
        public int Id { get; set; }
        public int SelectId { get; set; }
        public string Value { get; set; }

        public TDocSelect? Select { get; set; }

        public TDocOption(string value)
        {
            Value = value;
        }
    }
}