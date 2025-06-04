namespace Lnu_web.Models.ScheduleModel
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public int PairNumber { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
    }
}
