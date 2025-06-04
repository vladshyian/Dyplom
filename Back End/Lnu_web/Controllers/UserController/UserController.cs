using Lnu_web.Models.User;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;
using Lnu_web.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Lnu_web.Controllers.UserController
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/getStudent/{id}")]
        public async Task<ActionResult<StudentModel>> GetStudent(Guid id)
        {
            var userInfo = await _userService.GetStudentUser(id);

            if(userInfo != null)
            {
                return Ok(userInfo);
            }

            return NotFound("Not found");
        }

        [HttpGet("/getTeacher/{id}")]
        public async Task<ActionResult<TeacherModel>> GetTeacher(Guid id)
        {
            var userInfo = await _userService.GetTeacherUser(id);

            if(userInfo != null)
            {
                return Ok(userInfo);
            }

            return NotFound("Not found");
        }

        [HttpGet("/getAllUsers/{id}")]
        public async Task<ActionResult<List<UserModel>>> GetUsers(string id)
        {
            var users = await _userService.GetUsers(id);
            
            if(users != null)
            {
                return Ok(users);
            }

            return NotFound();
        }

        [HttpGet("/getStudentsByGroup/{group}")]
        public async Task<ActionResult<List<string>>> getStudentsByGroup(string group)
        {
            var users = await _userService.GetStudentsByGroup(group);

            return Ok(users);
        }
    }
}
