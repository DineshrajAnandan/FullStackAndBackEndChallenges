using TimeTableScheduler.Common.Models;

namespace TimeTableScheduler.Common.Services.Contracts
{
    public interface IClassService
    {
        IEnumerable<ClassViewModel> GetClasses();
        ClassViewModel? GetClass(int id);
        Task<int> CreateClass(ClassViewModel obj);
        Task<bool> DeleteClass(int id);
    }
}
