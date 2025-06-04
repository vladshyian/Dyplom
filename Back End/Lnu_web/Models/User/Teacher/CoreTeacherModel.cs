namespace Lnu_web.Models.User.Teacher
{
    public class CoreTeacherModel
    {
        public Guid Id { get; set; }
        public int DepartamentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
