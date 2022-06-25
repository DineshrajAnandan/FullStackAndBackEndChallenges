using TimeTableScheduler.Common.Models;
using TimeTableScheduler.Common.Services.Contracts;
using TimeTableScheduler.Repository.Context;

namespace TimeTableScheduler.Common.Services
{
    public class ClassService: IClassService
    {
        private readonly SchoolDbContext _dbContext;
        public ClassService(SchoolDbContext schoolDbContext)
        {
            _dbContext = schoolDbContext;
        }

        public IEnumerable<ClassViewModel> GetClasses()
        {
            var classesLookup = _dbContext.Class
               .Join(_dbContext.Subject,
                   o => o.Id,
                   i => i.ClassId,
                   (o, i) => new
                   {
                       Id = o.Id,
                       Number = o.Number,
                       Strength = o.Strength,
                       Subject = i.Name
                   })
               .ToLookup(i => i.Id);


            return classesLookup.Select(item => new ClassViewModel
            {
                Id = item.Key,
                Number = item.First().Number,
                Strength = item.First().Strength,
                Subjects = item.Select(x => x.Subject).ToList()
            });
        }

        public ClassViewModel? GetClass(int id)
        {
            var clazzLookup = _dbContext.Class
                .Where(c => c.Id == id)
                  .Join(_dbContext.Subject,
                      o => o.Id,
                      i => i.ClassId,
                      (o, i) => new
                      {
                          Id = o.Id,
                          Number = o.Number,
                          Strength = o.Strength,
                          Subject = i.Name
                      })
                  .ToLookup(i => i.Id);


            return clazzLookup.Select(item => new ClassViewModel
                {
                    Id = item.Key,
                    Number = item.First().Number,
                    Strength = item.First().Strength,
                    Subjects = item.Select(x => x.Subject).ToList()
                })
              .FirstOrDefault();
        }

        public async Task<int> CreateClass(ClassViewModel obj)
        {
            var newClass = new Repository.Entities.Class
            {
                Strength = obj.Strength,
                Number = obj.Number,
            };
            await _dbContext.Class.AddAsync(newClass);
            _dbContext.SaveChanges();

            foreach (var subject in obj.Subjects)
            {
                await _dbContext.Subject.AddAsync(new Repository.Entities.Subject
                {
                    ClassId = newClass.Id,
                    Name = subject
                });
            }
            _dbContext.SaveChanges();

            return newClass.Id;
        }

        public async Task<bool> DeleteClass(int id)
        {
            var clazz = _dbContext.Class.Where(c => c.Id == id).FirstOrDefault();
            if (clazz == null)
                return false;

            var subjects = _dbContext.Subject.Where(s => s.ClassId == clazz.Id).ToList();
            _dbContext.Subject.RemoveRange(subjects);
            await _dbContext.SaveChangesAsync();
            _dbContext.Class.Remove(clazz);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
