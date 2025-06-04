using Lnu_web.Models.User;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Interfaces.Services.IUserSerivce
{
    public interface IUserService
    {
        public Task<StudentModel> GetStudentUser(Guid studentId);
        public Task<TeacherModel> GetTeacherUser(Guid teacherId);
        Task<List<UserModel>> GetUsers(string userId);
    }
}
