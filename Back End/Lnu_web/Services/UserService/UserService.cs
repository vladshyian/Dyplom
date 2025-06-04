using Lnu_web.Data;
using Lnu_web.Interfaces.Services.IUserSerivce;
using Lnu_web.Models.User;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;
using Lnu_web.Reposotories.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
             _userRepository = userRepository;
        }

        public async Task<StudentModel> GetStudentUser(Guid studentId)
        {
            return await _userRepository.GetStudentUser(studentId);
        }

        public async Task<TeacherModel> GetTeacherUser(Guid teacherId)
        {
            return await _userRepository.GetTeacherUser(teacherId);
        }
        public async Task<List<UserModel>> GetUsers(string userId)
        {
            var users = await _userRepository.GetUsers(userId);

            if(users == null)
            {
                return null;
            }

            return users;
        }
        public async Task<List<string>> GetStudentsByGroup(string groupName)
        {
            var studentIds = await _userRepository.GetStudentsByGroup(groupName);

            return studentIds;
        }
    }
}
