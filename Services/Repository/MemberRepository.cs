using MadhurAPI.Data;
using MadhurAPI.Models;
using MadhurAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MadhurAPI.Services.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private AppDbContext _dbContext; 
        public MemberRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Member>> GetMembers()
        {
            var members = await _dbContext.Members.ToListAsync();
            return members;
        }
        public async Task<Member> GetMember(string MemberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == MemberId);
            return member;
        }

        public async Task<Member> AddMember(Member member)
        {
            var result = _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateMember(Member member)
        {
            var result = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == member.MemberId);
            if (result != null)
            {
                result.MemberName = member.MemberName;
                result.Address = member.Address;
                result.State = member.State;
                result.City = member.City;
            }
            await _dbContext.SaveChangesAsync();
            return member;
        }
        public async void DeleteMember(string MemberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == MemberId);
            if (member != null)
            {
                _dbContext.Members.Remove(member);
                await _dbContext.SaveChangesAsync();
            }
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
