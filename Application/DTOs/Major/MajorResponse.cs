using Application.DTOs.Corhot;
using Application.DTOs.Course;
using System.Text.Json.Serialization;

namespace Application.DTOs.Major
{
    public class MajorResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CourseResponse>? Courses { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CohortResponse>? Cohorts{ get; set; }
    }
}
