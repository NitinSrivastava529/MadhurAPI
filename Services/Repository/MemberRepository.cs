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
        public async Task<IEnumerable<RegKey>> RegKeys()
        {
            var RegKeys = await _dbContext.RegKeys.ToListAsync();
            return RegKeys;
        }
        public async Task<Member> GetMember(string memberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
            return member;
        }

        public async Task<string> AddMember(Member member)
        {
            if (_dbContext.RegKeys.Count(x => x.Key == member.RegPin) == 0)
                return "Registration Pin Not Valid";

            if (_dbContext.Members.Count(x => x.RegPin == member.RegPin) > 0)
                return "This Registration Pin Already Used";

            if (_dbContext.Members.Count(x => x.MobileNo == member.MobileNo) > 0)
                return "Mobile No Already Exists";

            if (_dbContext.Members.Count(x => x.AadharNo == member.AadharNo) > 0)
                return "AadharNo No Already Exists";

            member.MemberId = "MEM" + $"{(_dbContext.Members.Count() + 1):D7}";

            var result = _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            if (result.IsCompleted)
            {
                var dataKey = await _dbContext.RegKeys.FirstOrDefaultAsync(x => x.Key == member.RegPin);
                if (dataKey != null)
                    _dbContext.RegKeys.Remove(dataKey);

                await _dbContext.SaveChangesAsync();
            }
            return "Successfully Registered:" + member.MemberId;
        }
        public async Task<string> UpdateStatus(string memberId)
        {
            var result = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
            if (result != null)
            {
                result.IsActive = result.IsActive.Equals('Y') ? 'N' : 'Y';
                await _dbContext.SaveChangesAsync();
            }
            return "Success";
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
                result.PinCode = member.PinCode;

            }
            await _dbContext.SaveChangesAsync();
            return member;
        }
        public void DeleteMember(string memberId)
        {
            var member = _dbContext.Members.FirstOrDefault(x => x.MemberId == memberId);
            if (member != null)
            {
                _dbContext.Members.Remove(member);
                _dbContext.SaveChangesAsync();
            }           
        }
        public async Task<string> GenerateKey()
        {
            string key = NewRegKey();
            int count = await _dbContext.RegKeys.CountAsync(x => x.Key == key) + await _dbContext.Members.CountAsync(x => x.RegPin == key);
            if (count == 0)
            {
                var keys = new RegKey()
                {
                    Key = key,
                    CreationDate = DateTime.Now
                };
                await _dbContext.RegKeys.AddAsync(keys);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                await GenerateKey();
            }
            return key;
        }
        public string NewRegKey()
        {
            Random rnd = new Random();
            string AllowedString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            char[] chars = new char[10];
            for (int i = 0; i < 10; i++)
            {
                chars[i] = AllowedString[rnd.Next(0, AllowedString.Length)];
            }
            return new string(chars).ToLower();
        }
    }
}
