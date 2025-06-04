namespace Lnu_web.Models.User.Teacher
{
    public class TeacherModel
    {
        public CoreTeacherModel CoreInfo { get; set; }
        public AcademicalTeacherModel AcademicDetails { get; set; }
        public AdditionalTeacherModel AdditionalInfo { get; set; }
    }
}
