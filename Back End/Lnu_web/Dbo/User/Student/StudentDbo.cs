namespace Lnu_web.Dbo.User.Student
{
    public class StudentDbo
    {

        public Guid? Id { get; set; }
        public AcademicalStudentDbo AcademicalStudent { get; set; }
        public CoreStudentDbo CoreStudent { get; set; }
    }
}
