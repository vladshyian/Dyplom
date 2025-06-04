using System.Text.Json.Serialization;

namespace Lnu_web.Dbo.Schedule
{
    public class Schedule
    {
        public int Id { get; set; }     
        public int GroupId { get; set; }  
        public string DayOfWeek { get; set; }  
        public int PairNumber { get; set; }  
        public string Time { get; set; }    
        public string Name { get; set; }
    }

}
