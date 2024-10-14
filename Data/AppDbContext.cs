using MadhurAPI.Models;
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
    }
}
