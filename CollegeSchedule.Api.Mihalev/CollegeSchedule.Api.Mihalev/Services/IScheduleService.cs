using CollegeSchedule.Api.Mihalev.DTO;

namespace CollegeSchedule.Api.Mihalev.Services
{
    public interface IScheduleService
    {
        Task<List<ScheduleByDateDto>> GetScheduleForGroup(string groupName, DateTime startDate, DateTime endDate);
    }
}