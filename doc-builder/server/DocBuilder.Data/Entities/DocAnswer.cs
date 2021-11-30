namespace DocBuilder.Data.Entities
{
    public class DocAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Value { get; set; }

        public DocQuestion? Question { get; set; }

        public DocAnswer(string value)
        {
            Value = value;
        }
    }
}