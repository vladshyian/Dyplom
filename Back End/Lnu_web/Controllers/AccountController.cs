using Microsoft.AspNetCore.Mvc;
using Lnu_web.Dbo.User.Student;
using Lnu_web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Lnu_web.Dbo.User;
using Lnu_web.Dbo.User.Teacher;
using Lnu_web.Dbo.Departament;

namespace Lnu_web.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // POST: /Account/AddStudent
        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDbo studentDbo)
        {
            if (studentDbo == null || studentDbo.AcademicalStudent == null || studentDbo.CoreStudent == null)
            {
                return BadRequest("Invalid student data.");
            }

            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentDbo.Id);

            if (existingStudent != null)
            {
                return BadRequest("Student with this ID already exists.");
            }

            _context.Students.Add(studentDbo);
            await _context.SaveChangesAsync();

            var user = new ApplicationUser
            {
                UserName = studentDbo.CoreStudent.Email,
                Email = studentDbo.CoreStudent.Email,
                PhoneNumber = studentDbo.CoreStudent.Phone,
                StudentId = studentDbo.Id, 
            };

            var result = await _userManager.CreateAsync(user, "passworD@123");

            if (!result.Succeeded)
            {
                string errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Error creating user: {errorMessages}");
            }

            var roleExists = await _roleManager.RoleExistsAsync("Student");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }

            await _userManager.AddToRoleAsync(user, "Student");

            return CreatedAtAction(nameof(AddStudent), new { id = studentDbo.Id }, studentDbo);
        }

        [HttpPost("addDepartament")]
        public async Task<IActionResult> AddDepartament([FromBody] DepartamentDbo departamentDbo)
        {
            if (departamentDbo == null)
            {
                return BadRequest(new { message = "Invalid departament object" });
            }

            _context.departaments.Add(departamentDbo);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherDbo teacherDbo)
        {
            if (teacherDbo == null || teacherDbo.CoreInfo == null || teacherDbo.AcademicDetails == null)
            {
                return BadRequest("Invalid teacher data.");
            }

            var existingTeacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherDbo.Id);

            if (existingTeacher != null)
            {
                return BadRequest("Teacher with this ID already exists.");
            }

            teacherDbo.Id = Guid.NewGuid();
            teacherDbo.CoreInfo.Id = teacherDbo.Id;
            teacherDbo.AcademicDetails.Id = teacherDbo.Id;
            teacherDbo.AdditionalInfo.Id = teacherDbo.Id;


            _context.Teachers.Add(teacherDbo);
            await _context.SaveChangesAsync(); 

            var user = new ApplicationUser
            {
                UserName = teacherDbo.CoreInfo.Email,
                Email = teacherDbo.CoreInfo.Email,
                PhoneNumber = teacherDbo.CoreInfo.Phone,
                TeacherId = teacherDbo.Id, 
            };

            var result = await _userManager.CreateAsync(user, "securePass@123");

            if (!result.Succeeded)
            {
                string errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Error creating user: {errorMessages}");
            }

            var roleExists = await _roleManager.RoleExistsAsync("Teacher");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Teacher"));
            }

            await _userManager.AddToRoleAsync(user, "Teacher");

            return CreatedAtAction(nameof(AddTeacher), new { id = teacherDbo.Id }, teacherDbo);
        }
    }
}

