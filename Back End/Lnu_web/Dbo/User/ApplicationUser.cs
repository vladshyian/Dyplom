using Lnu_web.Dbo.User.Student;
using Lnu_web.Dbo.User.Teacher;
using Microsoft.AspNetCore.Identity;

namespace Lnu_web.Dbo.User
{
    public class ApplicationUser :IdentityUser 
    { 
        public Guid? StudentId { get; set; }
        public StudentDbo Student { get; set; }

        public Guid? TeacherId { get; set; }
        public TeacherDbo Teacher { get; set; }
    }
}
