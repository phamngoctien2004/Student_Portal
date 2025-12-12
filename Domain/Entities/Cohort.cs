// lop nien che
namespace Domain.Entities
{
    public class Cohort
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public int FacultyId { get; set; }
        //public Faculty? Faculty { get; set; }
        public int MajorId { get; set; }
        public Major? Major { get; set; }
        public ICollection<Student> Students { get; set; } = [];
    }
}
