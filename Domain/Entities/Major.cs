namespace Domain.Entities
{
    public class Major
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public ICollection<Course> Courses { get; set; } = [];
        public ICollection<Cohort> Cohorts { get; set; } = [];
    }
}
