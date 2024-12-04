using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace MadhurAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<RegKey> RegKeys { get; set; }
        public DbSet<StateMaster> StateMaster { get; set; }
        public DbSet<DistrictMaster> DistrictMaster { get; set; }
        public DbSet<RewardMaster> RewardMaster { get; set; }
        public DbSet<BannerMaster> BannerMaster { get; set; }
        public DbSet<RewardDistributor> RewardDistributor { get; set; }
        public DbSet<YoutubeVideo> YoutubeVideo { get; set; }
        public DbSet<TermsCondition> TermsCondition { get; set; }

        //DTOs
        public DbSet<AllMemberDTO> AllMember { get; set; }
        public DbSet<AllSelfMemberDTO> AllSelfMember { get; set; }
        public DbSet<LevelCount> LevelCount { get; set; }
        public DbSet<LevelWiseMemberDTO> LevelWiseMember { get; set; }
        public DbSet<LevelReportDTO> LevelReport { get; set; }
        public DbSet<RewardMasterDTO> RewardMasterDTO { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BannerMaster>().HasData(
                new BannerMaster() { AutoId = 1, Banner = "slider1.png" },
                new BannerMaster() { AutoId = 2, Banner = "slider2.png" }
                );
            modelBuilder.Entity<AllMemberDTO>().HasNoKey();
            modelBuilder.Entity<AllSelfMemberDTO>().HasNoKey();
            modelBuilder.Entity<LevelCount>().HasNoKey();
            modelBuilder.Entity<LevelWiseMemberDTO>().HasNoKey();
            modelBuilder.Entity<LevelReportDTO>().HasNoKey();
            modelBuilder.Entity<RewardMasterDTO>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
