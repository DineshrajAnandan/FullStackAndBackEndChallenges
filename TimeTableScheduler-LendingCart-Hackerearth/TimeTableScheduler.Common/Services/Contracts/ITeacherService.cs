using TimeTableScheduler.Common.Models;

namespace TimeTableScheduler.Common.Services.Contracts
{
    public interface ITeacherService
    {
        IEnumerable<TeacherViewModel> GetTeachers();
        TeacherViewModel? GetTeacher(int id);
        Task<int> CreateTeacher(TeacherViewModel req);
    }
}
