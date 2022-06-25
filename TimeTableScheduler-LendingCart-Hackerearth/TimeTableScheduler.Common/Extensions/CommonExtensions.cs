namespace TimeTableScheduler.Common.Extensions
{
    public static class CommonExtensions
    {
        public static IEnumerable<T> GetRangeOfItemsFromIndex<T>(this IEnumerable<T> list, int startIndex, int count)
        {
            var actualList = list.ToList();
            var newList = new List<T>();
            for (int i = startIndex; i < startIndex + count; i++)
            {
                int index = i % actualList.Count;
                newList.Add(actualList[index]);
            }
            return newList;
        }
    }
}
