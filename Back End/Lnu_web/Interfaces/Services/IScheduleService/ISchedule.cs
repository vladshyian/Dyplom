using Lnu_web.Models.ScheduleModel;
using Lnu_web.Models.User.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Interfaces.Services.IScheduleService
{
    public interface ISchedule
    {
        Task<List<ScheduleModel>> GetStudentSchedulesAsync(string groupName);
        Task<List<ScheduleModel>> GetTeacherScheduleAsync(string teacherId);
        Task<List<GroupModel>> GetGroups();
        Task<ScheduleModel> UpdateSchedule(int groupId, string dayOfWeek, ScheduleModel scheduleModel);
        Task<ScheduleModel> UpdateTeacherSchedule(string teacherId, string dayOfWeek, [FromBody] ScheduleModel updatedTeacherSchedule);
        Task<bool> DeleteTeacherSchedule(DeleteSchedule deleteSchedule);
    }
}
