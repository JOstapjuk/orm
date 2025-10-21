namespace orm.Models
{
    public class Document
    {
        public int Id { get; set; }

        public string Type { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int PersonId { get; set; }
    }
}
