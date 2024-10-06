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
        public async Task<Member> GetMember(string memberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
            return member;
        }

        public async Task<string> AddMember(Member member)
        {
            if (_dbContext.Members.Count(x => x.RegPin == member.RegPin) > 0)
                return "Registration Pin Not Valid";

            if (_dbContext.Members.Count(x => x.MobileNo == member.MobileNo) > 0)
                return "Mobile No Already Exists";

            if (_dbContext.Members.Count(x => x.AadharNo == member.AadharNo) > 0)
                return "AadharNo No Already Exists";

            member.MemberId = "MEM00000" + _dbContext.Members.Count() + 1;

            var result = _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return "Successfully Registered";
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
        public async void DeleteMember(string memberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
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
