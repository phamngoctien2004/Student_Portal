using Core.Entities;

namespace Domain.Entities
{
    public class Student : Person
    {
        public int Id { get; set; }
        
        public string CohortName { get; set; } = string.Empty;

        public int CohortId { get; set; }
        public Cohort? Cohort { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = [];
    }
}
