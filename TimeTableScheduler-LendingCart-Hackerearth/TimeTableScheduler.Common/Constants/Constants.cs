namespace TimeTableScheduler.Common.Constants
{
    public static class Constants
    {
        public static readonly string[] WorkingDays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        public static readonly string TimeTableCsvHeader = string.Join(", ", new [] {"Class", "Day", "Section1", "Section2", "Section3", "Section4"});
        public const string csvDownloadMimeType = "application/octet-stream";
        public const string csvDownloadName = "TimeTable.csv";
    }
}
