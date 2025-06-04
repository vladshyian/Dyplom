using Lnu_web.Data;
using Lnu_web.Dbo.Schedule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Lnu_web.Migrations;
using System.Text.RegularExpressions;

namespace Lnu_web.Controllers
{
    [ApiController]
    [Route("AddSchedule")]
    public class TempController : Controller
    {
        private readonly DataContext _context;

        public TempController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("/addSchedule")]
        public async Task<IActionResult> AddSchedule()
        {
            // Додавання групи (якщо вона ще не існує)
            var group1 = new Dbo.Schedule.Group
            {
                Id = 1,
                GroupName = "Фес-42"
            };

            _context.Groups.Add(group1);
            await _context.SaveChangesAsync();

            // Створення розкладу для цієї групи
            var scheduleData = new List<Schedule>
            {
                // Понеділок
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Понеділок",
                    PairNumber = 1,
                    Time = "8:30-9:50",
                    Name = "Цифрове опрацювання зображень<br>Лабораторна робота<br>Підгурпа 1"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Понеділок",
                    PairNumber = 3,
                    Time = "10:10-11:30",
                    Name = "Теорія прийняття рішень<br>Лабораторна робота<br>Підгурпа 1"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Понеділок",
                    PairNumber = 5,
                    Time = "11:50-13:10",
                    Name = "Цифрове опрацювання зображень<br>Лекція"
                },
                // Середа
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Середа",
                    PairNumber = 1,
                    Time = "8:30-9:50",
                    Name = "Системи нечіткої логіки<br>Лабораторна робота<br>Підгурпа 1<br><br>Системи нечіткої логіки<br>Лабораторна робота<br>Підгурпа 2"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Середа",
                    PairNumber = 2,
                    Time = "10:10-11:30",
                    Name = "Системи нечіткої логіки<br>Лекція"
                },
                // Четвер
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Четвер",
                    PairNumber = 3,
                    Time = "11:50-13:10",
                    Name = "Нейронні мережі<br>Лабораторна робота<br>Підгрупа 1"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Четвер",
                    PairNumber = 4,
                    Time = "13:30-14:50",
                    Name = "Управління ІТ-проектами<br>Лекція<br>Потік"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Четвер",
                    PairNumber = 5,
                    Time = "15:05-16:25",
                    Name = "Нейронні мережі<br>Лабораторна робота<br>Підгрупа 2"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Четвер",
                    PairNumber = 5,
                    Time = "16:40-18:00",
                    Name = "Теорія прийняття рішень<br>Лабораторна робота<br>Підгрупа 2"
                },
                // П'ятниця
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "П'ятниця",
                    PairNumber = 6,
                    Time = "16:40-18:00",
                    Name = "Управління ІТ-проектами (Менеджмент)<br>Лабораторна робота<br>Підгрупа 1<br><br>Цифрове опрацювання зображень<br>Лабораторна робота<br>Підгрупа 2"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "П'ятниця",
                    PairNumber = 7,
                    Time = "18:10-19:30",
                    Name = "Розпізнавання образів<br>Лабораторна робота<br>Підгрупа 1<br><br>Управління ІТ-проектами (Менеджмент)<br>Лабораторна робота<br>Підгрупа 2"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "П'ятниця",
                    PairNumber = 8,
                    Time = "19:40-21:00",
                    Name = "Розпізнавання образів<br>Лабораторна робота<br>Підгрупа 2"
                },
                // Субота
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Субота",
                    PairNumber = 3,
                    Time = "11:50-13:10",
                    Name = "Нейронні мережі<br>Лекція"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Субота",
                    PairNumber = 7,
                    Time = "13:30-14:50",
                    Name = "Розпізнавання образів<br>Лекція<br>Потік"
                },
                new Schedule
                {
                    GroupId = group1.Id,
                    DayOfWeek = "Субота",
                    PairNumber = 8,
                    Time = "19:40-21:00",
                    Name = "Теорія прийняття рішень<br>Лекція<br>Потік"
                }
            };

            _context.Schedules.AddRange(scheduleData);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Розклад успішно додано!" });
        }

        [HttpPost("/addTeacherSchedule")]
        public async Task<IActionResult> AddTeacherSchedule()
        {
            var scheduleData = new List<Dbo.Schedule.TeacherSchedule>
            {
                new Dbo.Schedule.TeacherSchedule
                {
                    TeacherId = "33785336-b79e-423d-a729-41bd37c6034f",
                    DayOfWeek = "Середа",
                    PairNumber = 2,
                    Time = "10:10 - 11:30",
                    Name = "Комп'ютерна лінгвістика<br>Лекція<br>Потік ФЕС-31с, ФЕС-32с<br>130/Т"
                }
            };

            _context.teacherSchedules.AddRange(scheduleData);
            await _context.SaveChangesAsync();

            return Ok("Розклад додано!");
        }

        [HttpGet("/getGroupIdByName/{groupName}")]
        public async Task<IActionResult> GetGroupIdByName(string groupName)
        {
            var group = await _context.Groups
                                       .Where(g => g.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase))
                                       .FirstOrDefaultAsync();

            if (group == null)
            {
                return NotFound(new { message = "Група не знайдена." });
            }

            return Ok(new { groupId = group.Id });
        }

        [HttpGet("/getScheduleByGroupName/{groupName}")]
        public async Task<IActionResult> GetScheduleByGroupName(string groupName)
        {
            // Спочатку шукаємо групу за її назвою
            var group = await _context.Groups
                                       .Where(g => g.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase))
                                       .FirstOrDefaultAsync();

            if (group == null)
            {
                return NotFound(new { message = "Група не знайдена." });
            }

            // Тепер отримуємо розклад для знайденої групи
            var schedule = await _context.Schedules
                                         .Where(s => s.GroupId == group.Id)
                                         .OrderBy(s => s.PairNumber) // Сортуємо за номерами пар
                                         .ToListAsync();

            if (!schedule.Any())
            {
                return NotFound(new { message = "Розклад для цієї групи не знайдено." });
            }

            

            return Ok(schedule);
        }


    }
}
