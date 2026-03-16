using CollegeSchedule.Api.Mihalev.Data;
using CollegeSchedule.Api.Mihalev.Services;  
using CollegeSchedule.Api.Mihalev.Middlewares; 
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Загружаем переменные из .env файла
        DotNetEnv.Env.Load();

        // Собираем строку подключения из переменных окружения
        var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                               $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                               $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                               $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                               $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

        // Регистрируем контекст базы данных с PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Регистрируем сервисы
        builder.Services.AddScoped<IScheduleService, ScheduleService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Настраиваем конвейер HTTP-запросов
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Добавляем middleware для обработки ошибок
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}