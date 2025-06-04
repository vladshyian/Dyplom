using System.ComponentModel.DataAnnotations;

namespace Lnu_web.Dbo.Schedule
{
    public class TeacherSchedule
    {
        [Key]
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string DayOfWeek { get; set; }
        public int PairNumber { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
    }
}
