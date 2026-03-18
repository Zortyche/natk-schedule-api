using CollegeSchedule.Api.Mihalev.Services;
using CollegeSchedule.Api.Mihalev.DTO;
using CollegeSchedule.Api.Mihalev.Data;  // Добавьте для AppDbContext
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // Добавьте для Entity Framework методов

namespace CollegeSchedule.Api.Mihalev.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        private readonly AppDbContext _db;  // Добавляем поле для контекста БД

        // Обновляем конструктор - добавляем AppDbContext
        public ScheduleController(IScheduleService service, AppDbContext db)
        {
            _service = service;
            _db = db;
        }

        // Существующий метод для получения расписания
        [HttpGet("{groupName}")]
        public async Task<ActionResult<List<ScheduleByDateDto>>> GetSchedule(
            string groupName,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var result = await _service.GetScheduleForGroup(groupName, start, end);
            return Ok(result);
        }

      
        // Получает список всех групп из базы данных
        [HttpGet("groups")]
        public async Task<ActionResult<List<string>>> GetGroups()
        {
            try
            {
                var groups = await _db.StudentGroups
                    .OrderBy(g => g.GroupName)  // Сортируем по алфавиту
                    .Select(g => g.GroupName)   // Берем только названия групп
                    .ToListAsync();

                return Ok(groups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении списка групп: {ex.Message}");
            }
        }
    }
}