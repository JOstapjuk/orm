namespace orm.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int ClassroomId { get; set; }
    }
}
