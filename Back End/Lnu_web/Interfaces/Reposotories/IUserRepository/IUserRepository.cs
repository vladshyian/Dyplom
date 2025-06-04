using Lnu_web.Models.User;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Interfaces.Reposotories.IUserRepository
{
    public interface IUserRepository
    {
        Task<StudentModel> GetStudentUser(Guid studentID);
        Task<TeacherModel> GetTeacherUser(Guid teacherID);
        Task<List<UserModel>> GetUsers(string userId);
    }
}
