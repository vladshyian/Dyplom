using AutoMapper;
using Lnu_web.Data;
using Lnu_web.Interfaces.Reposotories.IUserRepository;
using Lnu_web.Models.User;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lnu_web.Reposotories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext, IMapper mapper) 
        {  
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<StudentModel> GetStudentUser(Guid studentID)
        {
            var studentDbo = await _dataContext.Students
                .Where(s => s.Id == studentID)
                .Include(s => s.AcademicalStudent)
                .Include(s => s.CoreStudent)
                .FirstOrDefaultAsync();

            if (studentDbo != null)
            {
                    var studentModel = _mapper.Map<StudentModel>(studentDbo);

                    return studentModel;
            }

            return null;
        }

        public async Task<TeacherModel> GetTeacherUser(Guid teacherID)
        {
            var teacherDbo = await _dataContext.Teachers
                 .Where(s => s.Id == teacherID)
                 .Include(s => s.AcademicDetails)
                 .Include(s => s.CoreInfo)
                 .Include(s => s.AdditionalInfo)
                 .FirstOrDefaultAsync();

            if (teacherDbo != null)
            {
                var teacherModel = _mapper.Map<TeacherModel>(teacherDbo);

                return teacherModel;
            }

            return null;
        }

        public async Task<List<UserModel>> GetUsers(string userId)
        {
            var students = await _dataContext.Students.Include(s => s.CoreStudent).ToListAsync();

            var teachers = await _dataContext.Teachers.Include(s => s.CoreInfo).ToListAsync();

            var users = new List<UserModel>();

            users.AddRange(students.Select(s => new UserModel
            {
                Id = s.Id.ToString(),
                RecieverName = s.CoreStudent.Name
            }));

            users.AddRange(teachers.Select(s => new UserModel
            {
                Id = s.Id.ToString(),
                RecieverName = s.CoreInfo.Name,
                UserPhoto = s.CoreInfo.PhotoPath
            }));

            var filteredUsers = users.Where(u => u.Id != userId).ToList();

            return filteredUsers;
        }

        public async Task<List<string>> GetStudentsByGroup(string groupName)
        {
            var studentIds = await _dataContext.Students
                 .Where(s => s.AcademicalStudent.Group == groupName)
                 .Select(s => s.Id.ToString()) 
                 .ToListAsync();

            return studentIds;
        }
    }
}
