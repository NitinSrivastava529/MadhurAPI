using MadhurAPI.Data;
using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using System;
using MadhurAPI.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using AutoMapper;

namespace MadhurAPI.Services.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public MemberRepository(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MemberDTO>> GetMembers(FilterDTO obj)
        {   
            var members = await (from mem in _dbContext.Members
                                 where (!string.IsNullOrEmpty(obj.Mobile) ? mem.MobileNo.Contains(obj.Mobile) : mem.MobileNo != null)
                                 orderby mem.RefId select mem).AsNoTracking().Skip((obj.PageNo - 1) * obj.PageSize).Take(obj.PageSize).ToListAsync();
            return _mapper.Map<IEnumerable<MemberDTO>>(members);
        }
        public async Task<IEnumerable<MemberDTO>> GetTodayMembers()
        {
            var members = await (from mem in _dbContext.Members                               
                                 where mem.CreationDate.Value.Date == DateTime.UtcNow.Date
                                 select mem).ToListAsync();
            return _mapper.Map<IEnumerable<MemberDTO>>(members);
        }
        public async Task<TotalCount> TotalCount()
        {
            var result = new TotalCount()
            {
                Today = await _dbContext.Members.CountAsync(x => x.CreationDate.Value.Date == DateTime.UtcNow.Date),
                Total = await _dbContext.Members.CountAsync(),
                Active = await _dbContext.Members.CountAsync(x => x.IsActive == 'Y'),
                Deactive = await _dbContext.Members.CountAsync(x => x.IsActive == 'N')
            };
            return result;
        }
        public async Task<IEnumerable<RegKey>> RegKeys()
        {
            var RegKeys = await _dbContext.RegKeys.OrderByDescending(x => x.AuotId).ToListAsync();
            return RegKeys;
        }
        public async Task<IEnumerable<StateMaster>> GetState()
        {
            var state = await _dbContext.StateMaster.ToListAsync();
            return state;
        }
        public async Task<IEnumerable<DistrictMaster>> GetDistrict(int state_code)
        {
            var district = await _dbContext.DistrictMaster.Where(x => x.state_code == state_code).ToListAsync();
            return district;
        }
        public async Task<Member> GetMember(string memberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
            return member;
        }
        public async Task<RegistrationDTO> AddMember(Member member)
        {
            var response = new RegistrationDTO();
            if (_dbContext.RegKeys.Count(x => x.Key == member.RegPin) == 0)
            {
                response.Message = "Registration Pin Not Valid";
                return response;
            }

            if (_dbContext.Members.Count(x => x.RegPin == member.RegPin) > 0)
            {
                response.Message = "This Registration Pin Already Used";
                return response;
            }

            if (_dbContext.Members.Count(x => x.MobileNo == member.MobileNo) > 0)
            {
                response.Message = "Mobile No Already Exists";
                return response;
            }

            if (_dbContext.Members.Count(x => x.AadharNo == member.AadharNo) > 0)
            {
                response.Message = "AadharNo No Already Exists";
                return response;
            }

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
            response.MemberId = member.MemberId;
            response.Password = member.Password;
            response.Message = "Successfully Registered.\n Login Id : " + member.MemberId + " \n Password : " + member.Password;
            return response;
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
        public async Task<bool> UpdateRegKeys(int[] AutoId)
        {
            _dbContext.RegKeys.Where(x => AutoId.Contains(x.AuotId)).ToList().ForEach(a => a.IsCopy = 'Y');
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Response> GenerateKey()
        {
            var response = new Response();
            string key = NewRegKey();
            int count = await _dbContext.RegKeys.CountAsync(x => x.Key == key) + await _dbContext.Members.CountAsync(x => x.RegPin == key);
            if (count == 0)
            {
                var keys = new RegKey()
                {
                    Key = key,
                    CreationDate = DateTime.Now
                };
                response.message = key;
                response.result = true;
                await _dbContext.RegKeys.AddAsync(keys);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                await GenerateKey();
            }
            return response;
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
        //Store Procedure Recursive Data
        public async Task<IEnumerable<LevelCount>> LevelCount(string MemberId)
        {
            var data = await _dbContext.LevelCount.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},LevelTotalCount").ToListAsync();
            return data;
        }    
        public async Task<IEnumerable<AllMemberDTO>> AllMember(string MemberId)
        {
            var data = await _dbContext.AllMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},AllMember").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AllSelfMemberDTO>> AllSelfMember(string MemberId)
        {
            var data = await _dbContext.AllSelfMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},AllSelfMember").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AllMemberDTO>> TodayMember(string MemberId)
        {
            var data = await _dbContext.AllMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},TodayMember").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<LevelWiseMemberDTO>> LevelWiseMember()
        {
            var data = await _dbContext.LevelWiseMember.FromSqlInterpolated($"exec pRecursiveQueries {"-"},LevelWiseReport").ToListAsync();
            return data;
        }
    }
}
