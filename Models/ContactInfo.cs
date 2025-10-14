namespace orm.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
