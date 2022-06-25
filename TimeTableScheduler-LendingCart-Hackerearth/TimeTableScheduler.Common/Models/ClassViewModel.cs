namespace TimeTableScheduler.Common.Models
{
    public class ClassViewModel
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public int Strength { get; set; }
        public IEnumerable<string> Subjects { get; set; }
    }
}
