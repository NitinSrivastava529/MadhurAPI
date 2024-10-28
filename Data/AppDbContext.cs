using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace MadhurAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<RegKey> RegKeys { get; set; }
        public DbSet<StateMaster> StateMaster { get; set; }
        public DbSet<DistrictMaster> DistrictMaster { get; set; }
        public DbSet<RecursiveData> RecursiveData { get; set; }
        public DbSet<LevelCount> LevelCount { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecursiveData>().HasNoKey();
            modelBuilder.Entity<LevelCount>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
