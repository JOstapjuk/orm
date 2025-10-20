namespace orm.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? TeacherId { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }

        public DateTime EnrolledAt { get; set; }
    }
}
