using Lnu_web.Dbo.Schedule;
using Lnu_web.Dbo.User;
using Lnu_web.Models.ScheduleModel;
using Lnu_web.Services.ScheduleService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lnu_web.Controllers.ScheduleController
{
    [ApiController]
    [Route("Schedule")]
    public class ScheduleController : Controller
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("/getStudentSchedule/{groupName}")]
        public async Task<ActionResult<ScheduleModel>> getStudentSchedule(string groupName)
        {
            var schedule = await _scheduleService.GetStudentSchedulesAsync(groupName);

            if (schedule != null)
            {
                return Ok(schedule);
            }

            return NotFound();
        }

        [HttpGet("/getTeacherSchedule/{teacherId}")]
        public async Task<ActionResult<ScheduleModel>> getTeacherSchedule(string teacherId)
        {

            var schedule = await _scheduleService.GetTeacherScheduleAsync(teacherId);

            if (schedule != null)
            {
                return Ok(schedule);
            }

            return NotFound();
        }

        [HttpGet("/getGroups")]
        public async Task<ActionResult<GroupModel>> getGroups()
        {
            var groups = await _scheduleService.GetGroups();

            if(groups != null)
            {
                return Ok(groups);
            }

            return NotFound();
        }

        [HttpPut("updateSchedule/{groupId}/{dayOfWeek}")]
        public async Task<IActionResult> UpdateSchedule(int groupId, string dayOfWeek, [FromBody] ScheduleModel updatedSchedule)
        {
            if (updatedSchedule == null)
            {
                return BadRequest("Invalid schedule data.");
            }

            var schedule = await _scheduleService.UpdateSchedule(groupId, dayOfWeek, updatedSchedule);

            if (schedule == null)
            {
                return NotFound("Schedule not found.");
            }

            return Ok(new { message = "Schedule updated successfully." });
        }

        [HttpPut("updateTeacherSchedule/{teacherId}/{dayOfWeek}")]
        public async Task<IActionResult> UpdateTeacherSchedule(string teacherId, string dayOfWeek, [FromBody] ScheduleModel updatedTeacherSchedule)
        {
            if (updatedTeacherSchedule == null)
            {
                return BadRequest("Invalid teacher schedule data.");
            }

            var teacherSchedule = await _scheduleService.UpdateTeacherSchedule(teacherId, dayOfWeek, updatedTeacherSchedule);

            if (teacherSchedule == null)
            {
                return NotFound("Teacher schedule not found.");
            }

            return Ok("Teacher schedule updated successfully.");
        }

        [HttpGet("getTeacherSubjects/{teacherId}")]
        public async Task<ActionResult<List<ScheduleModel>>> GetTeacherSubjects(string teacherId)
        {
            var subjects = await _scheduleService.GetTeacherSubjects(teacherId);

            if(subjects == null)
            {
                return NotFound();
            }

            return subjects;
        }

        [HttpPost("deleteSchedule")]
        public async Task<ActionResult> DeleteSchedule([FromBody] DeleteSchedule deleteSchedule)
        {
            if (deleteSchedule == null)
            {
                return BadRequest("Invalid schedule ID.");
            }

            bool isDeleted = await _scheduleService.DeleteTeacherSchedule(deleteSchedule);

            if (isDeleted)
            {
                return Ok("Schedule deleted successfully.");
            }
            else
            {
                return NotFound("Schedule not found.");
            }
        }
    }
}
