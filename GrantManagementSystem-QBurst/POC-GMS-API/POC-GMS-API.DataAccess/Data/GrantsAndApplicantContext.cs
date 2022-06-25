using Microsoft.EntityFrameworkCore;
using POC_GMS_API.Models.DB;

namespace POC_GMS_API.DataAccess.Data
{
    public class GrantsAndApplicantContext: DbContext
    {
        public GrantsAndApplicantContext(DbContextOptions<GrantsAndApplicantContext> options)
            : base(options)
        {

        }

        public DbSet<GrantProgram> GrantProgram { get; set; }
        public DbSet<EducationalDetail> EducationalDetail { get; set; }
        public DbSet<ApplicantDetail> ApplicantDetail { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Application> Application { get; set; }
    }
}
