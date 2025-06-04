using Lnu_web.Interfaces.Services.IScheduleService;
using Lnu_web.Models.ScheduleModel;
using Lnu_web.Models.User.Teacher;
using Lnu_web.Reposotories.ScheduleRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lnu_web.Services.ScheduleService
{
    public class ScheduleService : ISchedule
    {

        private readonly ScheduleRepository _scheduleRepository;

        public ScheduleService(ScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public Task<List<ScheduleModel>> GetStudentSchedulesAsync(string groupName)
        {
            var schedule = _scheduleRepository.GetStudentSchedulesAsync(groupName);

            if(schedule != null)
            {
                return schedule;
            }

            return null;
        }

        public Task<List<ScheduleModel>> GetTeacherScheduleAsync(string teacherId)
        {
            var schedule = _scheduleRepository.GetTeacherScheduleAsync(teacherId);

            if(schedule != null)
            {
                return schedule;
            }

            return null;
        }

        public Task<List<GroupModel>> GetGroups()
        {
            var groups = _scheduleRepository.GetGroups();

            if (groups != null)
            {
                return groups;
            }

            return null;
        }

        public async Task<ScheduleModel> UpdateSchedule(int groupId, string dayOfWeek, ScheduleModel scheduleModel)
        {
            var schedule = await _scheduleRepository.UpdateSchedule(groupId, dayOfWeek, scheduleModel);

            if (schedule == null)
            {
                return null;
            }

            return scheduleModel;
        }

        public async Task<ScheduleModel> UpdateTeacherSchedule(string teacherId, string dayOfWeek, [FromBody] ScheduleModel updatedTeacherSchedule)
        {
            if (updatedTeacherSchedule == null)
            {
                return null;
            }

            
            var teacherSchedule = await _scheduleRepository.UpdateTeacherSchedule(teacherId, dayOfWeek, updatedTeacherSchedule);


            if (teacherSchedule == null)
            {
                return null;
            }

            return updatedTeacherSchedule;
        }

        public async Task<List<ScheduleModel>> GetTeacherSubjects(string teacherId)
        {
            var subjects = await _scheduleRepository.GetTeacherSubjects(teacherId);

            if (!subjects.Any())
            {
                return null;
            }

            return subjects;

        }

        public async Task<bool> DeleteTeacherSchedule(DeleteSchedule deleteSchedule)
        {
            var isDeleted = await _scheduleRepository.DeleteTeacherSchedule(deleteSchedule);

            return isDeleted;
        }
    }
}
