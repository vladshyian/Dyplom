using AutoMapper;
using Lnu_web.Data;
using Lnu_web.Dbo.Schedule;
using Lnu_web.Dbo.User;
using Lnu_web.Interfaces.Reposotories.IScheduleRepository;
using Lnu_web.Models.ScheduleModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lnu_web.Reposotories.ScheduleRepository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ScheduleRepository(DataContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> GetGroupId(string groupName)
        {
            var group = await _context.Groups
                                       .Where(g => g.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase))
                                       .FirstOrDefaultAsync();

            if (group == null)
            {
                return 0;
            }

            return group.Id;
        }

        public async Task<List<ScheduleModel>> GetStudentSchedulesAsync(string groupName)
        {
            var groupId = await GetGroupId(groupName);

            if (groupId == 0)
            {
                return null;
            }

            var schedule = await _context.Schedules
                                         .Where(s => s.GroupId == groupId)
                                         .OrderBy(s => s.PairNumber) 
                                         .ToListAsync();

            if (!schedule.Any())
            {
                return null;
            }

            var scheduleModel = _mapper.Map<List<ScheduleModel>>(schedule);

            return scheduleModel;
        }

        public async Task<List<ScheduleModel>> GetTeacherScheduleAsync(string id)
        {
           var schedule = await _context.teacherSchedules
                                                .Where(s=>s.TeacherId == id)
                                                .OrderBy (s => s.PairNumber)
                                                .ToListAsync();
            if (!schedule.Any())
            {
                return null;
            }

            var scheduleModel = _mapper.Map<List<ScheduleModel>>(schedule);

            return scheduleModel;
        }

        public async Task<List<GroupModel>> GetGroups()
        {
            var groups = await _context.Groups.ToListAsync();

            if(groups == null)
            {
                return null;
            }

            var groupList = _mapper.Map<List<GroupModel>>(groups);

            return groupList;
        }

        public async Task<ScheduleModel> UpdateSchedule(int groupId, string dayOfWeek, ScheduleModel scheduleModel)
        {
            var existingSchedule = await _context.Schedules
                .Where(s => s.GroupId == groupId && s.DayOfWeek == dayOfWeek && s.PairNumber == scheduleModel.PairNumber)
                .FirstOrDefaultAsync();

            if (existingSchedule != null)
            {
                existingSchedule.Name = scheduleModel.Name;
            }
            else
            {
                existingSchedule = new Dbo.Schedule.Schedule
                {
                    GroupId = groupId,
                    DayOfWeek = dayOfWeek,
                    PairNumber = scheduleModel.PairNumber,
                    Time = scheduleModel.Time,
                    Name = scheduleModel.Name
                };

                await _context.Schedules.AddAsync(existingSchedule);
            }

            await _context.SaveChangesAsync();

            return new ScheduleModel();
        }

        public async Task<ScheduleModel> UpdateTeacherSchedule(string teacherId, string dayOfWeek, [FromBody] ScheduleModel updatedTeacherSchedule)
        {
            if (updatedTeacherSchedule == null)
            {
                return null;
            }

            var existingTeacherSchedule = await _context.teacherSchedules
                                                        .Where(t => t.TeacherId == teacherId && t.DayOfWeek == dayOfWeek && t.PairNumber == updatedTeacherSchedule.PairNumber)
                                                        .FirstOrDefaultAsync();

            if (existingTeacherSchedule != null)
            {
                existingTeacherSchedule.Name = updatedTeacherSchedule.Name;
            }
            else
            {
                existingTeacherSchedule = new Dbo.Schedule.TeacherSchedule
                {
                    TeacherId = teacherId,
                    DayOfWeek = dayOfWeek,
                    Name = updatedTeacherSchedule.Name,
                    PairNumber = updatedTeacherSchedule.PairNumber,
                    Time = updatedTeacherSchedule.Time
                };

                await _context.teacherSchedules.AddAsync(existingTeacherSchedule);
            
            }

            await _context.SaveChangesAsync();

            return updatedTeacherSchedule;
        }

        public async Task<List<ScheduleModel>> GetTeacherSubjects(string teacherId)
        {
            var subjects = await _context.teacherSchedules.Where(s=>s.TeacherId.Equals(teacherId))
                                                    .ToListAsync();

            if(!subjects.Any())
            {
                return null;
            }

            var scheduleModel = _mapper.Map<List<ScheduleModel>>(subjects);

            return scheduleModel;

        }

        public async Task<bool> DeleteTeacherSchedule(DeleteSchedule deleteSchedule)
        {
            var schedule = await _context.teacherSchedules.FirstOrDefaultAsync(s => s.Id == deleteSchedule.Id);

            var studentSchedule = await _context.Schedules.Where(s => s.Name == deleteSchedule.Name && s.DayOfWeek == deleteSchedule.DayOfWeek && s.PairNumber == deleteSchedule.PairNumber)
                .FirstOrDefaultAsync();

            if(schedule == null || studentSchedule == null)
            {
                return false;
            }

            _context.teacherSchedules.Remove(schedule);

            _context.Schedules.Remove(studentSchedule);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}