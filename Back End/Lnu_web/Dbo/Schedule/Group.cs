namespace Lnu_web.Dbo.Schedule
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Schedule> Schedule { get; set; }
    }
}
