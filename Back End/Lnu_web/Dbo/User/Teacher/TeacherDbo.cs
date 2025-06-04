namespace Lnu_web.Dbo.User.Teacher
{
    public class TeacherDbo
    {
        public Guid Id { get; set; }
        public CoreTeacherDbo CoreInfo { get; set; }
        public AcademicalTeacherDbo AcademicDetails { get; set; }
        public AdditionalTeacherDbo AdditionalInfo { get; set; }
    }
}
