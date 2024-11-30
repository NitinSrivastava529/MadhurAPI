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
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper.Execution;
using Member = MadhurAPI.Models.Member;
using MadhurAPI.Migrations;

namespace MadhurAPI.Services.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public MemberRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MemberDTO>> GetMembers(FilterDTO obj)
        {
            var members = await (from mem in _dbContext.Members
                                 where (!string.IsNullOrEmpty(obj.Mobile) ? mem.MobileNo.Contains(obj.Mobile) : mem.MobileNo != null)
                                 || (!string.IsNullOrEmpty(obj.Mobile) ? mem.MemberId.Contains(obj.Mobile) : mem.MemberId != null)
                                 || (!string.IsNullOrEmpty(obj.Mobile) ? mem.Address.ToLower().Contains(obj.Mobile.ToLower()) : mem.Address != null)
                                 || (!string.IsNullOrEmpty(obj.Mobile) ? mem.State.ToLower().Contains(obj.Mobile.ToLower()) : mem.State != null)
                                 || (!string.IsNullOrEmpty(obj.Mobile) ? mem.City.ToLower().Contains(obj.Mobile.ToLower()) : mem.City != null)
                                 orderby mem.RefId
                                 select mem).AsNoTracking().Skip((obj.PageNo - 1) * obj.PageSize).Take(obj.PageSize).ToListAsync();
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

        public async Task<Member> GetMember(string memberId)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
            return member;
        }
        public async Task<IEnumerable<Member>> GetRepurchase(string memberId)
        {
            var member = await _dbContext.Members.Where(x => x.RefId == memberId && x.MemberType == "Repurchase").ToListAsync();
            return member;
        }
        public async Task<TotalRepurchaseDTO> TotalRepurchase()
        {
            var total = await _dbContext.Members.CountAsync(x => x.MemberType == "Repurchase");
            var today = await _dbContext.Members.CountAsync(x => x.MemberType == "Repurchase" && x.CreationDate.Value.Date == DateTime.UtcNow.Date);
            var data = await _dbContext.Members.Where(x => x.MemberType == "Repurchase" && x.CreationDate.Value.Date == DateTime.UtcNow.Date)
                .Select(x => new RepurchaseList()
                {
                    RefId = x.RefId,
                    RepurchaseId = x.MemberId,
                    RepurchaseName = x.MemberName
                }).ToListAsync();
            var result = new TotalRepurchaseDTO()
            {
                Total = total,
                Today = today,
                list = data
            };
            return result;
        }

        public async Task<Response> Repurchase(string MemberId, string RegKey)
        {
            var response = new Response();
            if (_dbContext.RegKeys.Count(x => x.Key == RegKey) == 0)
            {
                response.message = "Registration Pin Not Valid";
                response.result = false;
                return response;
            }

            if (_dbContext.Members.Count(x => x.RegPin == RegKey) > 0)
            {
                response.message = "This Registration Pin Already Used";
                response.result = false;
                return response;
            }

            var data = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == MemberId);
            if (data != null)
            {
                var count = (_dbContext.Members.Count(x => x.RefId == MemberId && x.MemberType == "Repurchase") + 1);
                data.AutoId = 0;
                data.RegPin = RegKey;
                data.RefId = MemberId;
                data.MemberId = data.MemberId + "-R" + count;
                data.MemberName = "Repurchase-" + count;
                data.MemberType = "Repurchase";
                data.CreationDate = DateTime.UtcNow;
            }
            var result = _dbContext.Members.AddAsync(data);
            await _dbContext.SaveChangesAsync();
            if (result.IsCompleted)
            {
                var dataKey = await _dbContext.RegKeys.FirstOrDefaultAsync(x => x.Key == RegKey);
                if (dataKey != null)
                    _dbContext.RegKeys.Remove(dataKey);

                await _dbContext.SaveChangesAsync();
            }
            response.message = "Successfully Repurchased";
            response.result = true;
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
            var data = await _dbContext.LevelCount.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},{"-"},LevelTotalCount").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AllMemberDTO>> AllMember(string MemberId)
        {
            var data = await _dbContext.AllMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},{"-"},AllMember").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AllSelfMemberDTO>> AllSelfMember(string MemberId)
        {
            var data = await _dbContext.AllSelfMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},{"-"},AllSelfMember").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AllSelfMemberDTO>> AllSelfMemberAdmin(string MemberId)
        {
            var data = await _dbContext.AllSelfMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},{"-"},AllSelfMemberAdmin").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AllMemberDTO>> TodayMember(string MemberId)
        {
            var data = await _dbContext.AllMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},{"-"},TodayMember").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<LevelReportDTO>> LevelReport()
        {
            var data = await _dbContext.LevelReport.FromSqlInterpolated($"exec pRecursiveQueries {"-"},{"-"},LevelReport").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<LevelWiseMemberDTO>> LevelWiseMember(string Prm1)
        {
            var data = await _dbContext.LevelWiseMember.FromSqlInterpolated($"exec pRecursiveQueries {"-"},{Prm1},LevelWiseReport").ToListAsync();
            return data;
        }
        //Reward Master Operation
        public async Task<Response> AddReward(RewardMasterDTO dto)
        {
            var response = new Response();
            if (_dbContext.RewardMaster.Count(x => x.MemberId == dto.MemberId && x.level == dto.level) > 0)
            {
                response.message = "This Reward Already Exists";
                response.result = false;
                return response;
            }

            var folderName = Path.Combine("Resource", "Reward");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var fileName = dto.MemberId + "_" + dto.level + "_" + dto.file_path.FileName;
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                dto.file_path.CopyTo(stream);
            }

            var data = _mapper.Map<RewardMaster>(dto);
            var result = _dbContext.RewardMaster.AddAsync(data);
            await _dbContext.SaveChangesAsync();
            if (result.IsCompletedSuccessfully)
            {
                response.message = "Successfully Saved.";
                response.result = true;
            }
            return response;
        }
        public async Task<IEnumerable<RewardMaster>> GetReward(string MemberId)
        {
            var data = await _dbContext.RewardMaster.Where(x => (MemberId != "ALL") ? x.MemberId == MemberId : x.MemberId != null).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<BannerMaster>> GetBanner()
        {
            return await _dbContext.BannerMaster.ToListAsync();
        }
        public async Task<Response> ApproveReward(RewardDistributorDTO obj)
        {
            var response = new Response();
            if (_dbContext.RewardMaster.Count(x => x.MemberId == obj.MemberId && x.level == obj.Level) == 0)
            {
                response.message = "This is Invalid Reward";
                response.result = false;
                return response;
            }
            if (obj.Level == "1")
            {
                if (_dbContext.RegKeys.Count(x => x.Key == obj.key) == 0)
                {
                    response.message = "Registration Pin Not Valid";
                    response.result = false;
                    return response;
                }
            }
            if (obj.Level == "2")
            {
                var keys = obj.key.Split("|");
                if (_dbContext.RegKeys.Count(x => keys.Contains(x.Key)) != 2)
                {
                    response.message = "Registration Pin Not Valid";
                    response.result = false;
                    return response;
                }
            }


            if (_dbContext.Members.Count(x => x.MemberId == obj.DistributorId) == 0)
            {
                response.message = "Invalid Distributor Id";
                response.result = false;
                return response;
            }
            var data = _mapper.Map<RewardDistributor>(obj);
            var result = _dbContext.RewardDistributor.AddAsync(data);

            if (result.IsCompletedSuccessfully)
            {
                if (obj.Level == "1")
                {
                    var dataKey = _dbContext.RegKeys.Where(x => x.Key == obj.key).FirstOrDefault();
                    if (dataKey != null)
                        _dbContext.RegKeys.Remove(dataKey);
                }
                if (obj.Level == "2")
                {
                    var keys = obj.key.Split("|");
                    var dataKey = _dbContext.RegKeys.Where(x => keys.Contains(x.Key)).ToList();
                    if (dataKey != null)
                        _dbContext.RegKeys.RemoveRange(dataKey);
                }

                var rewardInfo = _dbContext.RewardMaster.FirstOrDefault(x => x.MemberId == obj.MemberId && x.level == obj.Level);
                rewardInfo.IsApproved = 'Y';

                _dbContext.SaveChanges();
            }
            response.message = "Success";
            response.result = true;
            return response;
        }
        public async Task<IEnumerable<RewardDistributorInfoDTO>> RewardDistributorInfo(string distributorId)
        {
            var data = await _dbContext.RewardDistributor.Where(x => x.DistributorId == distributorId).ToListAsync();
            return _mapper.Map<IEnumerable<RewardDistributorInfoDTO>>(data);
        }
        public async Task<string> ResetDistributorReward(string distributorId)
        {
            var data = await _dbContext.RewardDistributor.Where(x => x.DistributorId == distributorId).ToListAsync();
            _dbContext.RewardDistributor.RemoveRange(data);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
        public async Task<TotalKeyDTO> TotalKey()
        {
            var res = new TotalKeyDTO()
            {
                member = await _dbContext.Members.CountAsync(x => x.MemberType == "Member"),
                purchase = await _dbContext.Members.CountAsync(x => x.MemberType == "Repurchase"),
                reward = await _dbContext.RewardDistributor.CountAsync(),
                total = 0
            };
            return res;
        }
        public async Task<string> DeleteReward(int AutoId)
        {
            var res = await _dbContext.RewardMaster.Where(x => x.AutoId == AutoId).FirstOrDefaultAsync();
            if (res == null)
                return "Not Found";
            _dbContext.RewardMaster.Remove(res);
            await _dbContext.SaveChangesAsync();

            return "Success";
        }
        public async Task<string> EditReward(int AutoId, string Remark)
        {
            var res = await _dbContext.RewardMaster.Where(x => x.AutoId == AutoId).FirstOrDefaultAsync();
            if (res == null)
                return "Not Found";
            res.Remark = Remark;

            _dbContext.RewardMaster.Update(res);
            await _dbContext.SaveChangesAsync();

            return "Success";
        }
    }
}
