namespace Lnu_web.Dbo.User.Teacher
{
    public class CoreTeacherDbo
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public int DepartamentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
