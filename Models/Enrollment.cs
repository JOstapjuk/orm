namespace orm.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public int TeacherId { get; set; }

        public DateTime EnrolledAt { get; set; }
    }
}
