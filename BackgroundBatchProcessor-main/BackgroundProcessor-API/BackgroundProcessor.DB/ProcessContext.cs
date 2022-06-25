using BackgroundProcessor.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace BackgroundProcessor.DB
{
    public class ProcessContext: DbContext
    {
        public ProcessContext(DbContextOptions<ProcessContext> options):
            base(options)
        {
                
        }

        public DbSet<BatchGroup> BatchesGroup { get; set; }
        public DbSet<Batch> Batch { get; set; }
    }
}
