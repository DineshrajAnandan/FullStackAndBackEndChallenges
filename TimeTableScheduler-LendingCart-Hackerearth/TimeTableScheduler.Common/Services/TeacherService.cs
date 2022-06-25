using TimeTableScheduler.Common.Models;
using TimeTableScheduler.Common.Services.Contracts;
using TimeTableScheduler.Repository.Context;
using TimeTableScheduler.Repository.Entities;

namespace TimeTableScheduler.Common.Services
{
    public class TeacherService: ITeacherService
    {
        private readonly SchoolDbContext _dbContext;

        public TeacherService(SchoolDbContext schoolDbContext)
        {
            _dbContext = schoolDbContext;
        }

        public IEnumerable<TeacherViewModel> GetTeachers()
        {
            return _dbContext.Teacher
                    .Join(_dbContext.Subject, o => o.SubjectId, i => i.Id, (o, i) =>
                      new TeacherViewModel
                      {
                          Id = o.Id,
                          Name = o.Name,
                          SubjectId = o.SubjectId,
                          SubjectName = i.Name
                      });
        }

        public TeacherViewModel? GetTeacher(int id)
        {
            return _dbContext.Teacher
                    .Join(_dbContext.Subject, o => o.SubjectId, i => i.Id, (o, i) =>
                     new TeacherViewModel
                     {
                         Id = o.Id,
                         Name = o.Name,
                         SubjectId = o.SubjectId,
                         SubjectName = i.Name
                     })
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
        }

        public async Task<int> CreateTeacher(TeacherViewModel req)
        {
            var newEntry = new Teacher
            {
                Name = req.Name,
                SubjectId = req.SubjectId,
            };
            _dbContext.Teacher.Add(newEntry);
            await _dbContext.SaveChangesAsync();
            return newEntry.Id;
        }
    }
}
