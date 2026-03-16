using CollegeSchedule.Api.Mihalev.Services;
using CollegeSchedule.Api.Mihalev.DTO; 
using Microsoft.AspNetCore.Mvc;

namespace CollegeSchedule.Api.Mihalev.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [HttpGet("{groupName}")]
        public async Task<ActionResult<List<ScheduleByDateDto>>> GetSchedule(  // Убрали DTO.
            string groupName,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var result = await _service.GetScheduleForGroup(groupName, start, end);
            return Ok(result);
        }
    }
}