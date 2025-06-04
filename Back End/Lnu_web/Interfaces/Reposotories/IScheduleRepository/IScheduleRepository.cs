using Lnu_web.Models.ScheduleModel;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Interfaces.Reposotories.IScheduleRepository
{
    public interface IScheduleRepository
    {
        Task<int> GetGroupId(string groupName);
        Task<List<ScheduleModel>> GetStudentSchedulesAsync(string groupName);
        Task<List<ScheduleModel>> GetTeacherScheduleAsync(string teacherId);
        Task<List<GroupModel>> GetGroups();
        Task<ScheduleModel> UpdateSchedule(int groupId, string dayOfWeek, ScheduleModel scheduleModel);
        Task<ScheduleModel> UpdateTeacherSchedule(string teacherId, string dayOfWeek, [FromBody] ScheduleModel updatedTeacherSchedule);
        Task<bool> DeleteTeacherSchedule(DeleteSchedule deleteSchedule);
    }
}
