using CollegeSchedule.Api.Mihalev.Models;

namespace CollegeSchedule.Api.Mihalev.DTO
{
    public class LessonDto
    {
        public int LessonNumber { get; set; }
        public string Time { get; set; } = null!;
        public Dictionary<LessonGroupPart, LessonPartDto?> GroupParts { get; set; } = new();
    }
}
