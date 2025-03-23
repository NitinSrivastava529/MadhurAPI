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
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Internal;
using MadhurAPI.Utility;

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
                                 || (!string.IsNullOrEmpty(obj.Mobile) ? mem.RegPin.ToLower().Contains(obj.Mobile.ToLower()) : mem.RegPin != null)
                                 orderby mem.CreationDate descending
                                 select mem).AsNoTracking().Skip((obj.PageNo - 1) * obj.PageSize).Take(obj.PageSize).ToListAsync();
            return _mapper.Map<IEnumerable<MemberDTO>>(members);
        }
        public async Task<IEnumerable<MemberDTO>> GetMembersInactive()
        {
            var members = await _dbContext.Members.Where(x => x.IsActive == 'N').AsNoTracking().ToListAsync();
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
        public async Task<IEnumerable<RegKey>> RegKeys(char param)
        {
            var RegKeys = await _dbContext.RegKeys.Where(x => x.IsCopy == param)
                            .OrderByDescending(x => x.AuotId).ToListAsync();
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
        public async Task<string> UpdateSubscribe(string memberId)
        {
            var result = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberId);
            if (result != null)
            {
                result.IsSubscribe = result.IsSubscribe.Equals('Y') ? 'N' : 'Y';
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
        public async Task<Member> UpdateMemberByAdmin(Member member)
        {
            var result = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == member.MemberId);
            if (result != null)
            {
                result.MemberName = member.MemberName;
                result.dob = member.dob;
                result.Address = member.Address;
                result.MobileNo = member.MobileNo;
                result.PinCode = member.PinCode;
                result.Nominee = member.Nominee;
                result.RelationWithNominee = member.RelationWithNominee;
            }
            await _dbContext.SaveChangesAsync();
            return member;
        }
        public async Task<Member> UpdateMemberByUser(Member member)
        {
            var result = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == member.MemberId);
            if (result != null)
            {
                result.MemberName = member.MemberName;
                result.MobileNo = member.MobileNo;
                result.dob = member.dob;
                result.AadharNo = member.AadharNo;
                result.State = member.State;
                result.City = member.City;
                result.Address = member.Address;
                result.PinCode = member.PinCode;
                result.Nominee = member.Nominee;
                result.RelationWithNominee = member.RelationWithNominee;
            }
            await _dbContext.SaveChangesAsync();
            return member;
        }
        public async Task<bool> UpdateRegKeys(string[] keys)
        {
            _dbContext.RegKeys.Where(x => keys.Contains(x.Key)).ToList().ForEach(a => a.IsCopy = 'Y');
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Response> GenerateKey(int NoOfKey)
        {
            List<string> list = new();
            var response = new Response();
            for (var i = 0; i < NoOfKey; i++)
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
                    list.Add(key);
                }
            }
            response.message = string.Join(",", list.ToArray());
            response.result = true;
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
        public async Task<IEnumerable<AllMemberDTO>> AllSelfMemberAdmin(string MemberId)
        {
            var data = await _dbContext.AllMember.FromSqlInterpolated($"exec pRecursiveQueries {MemberId},{"-"},AllSelfMemberAdmin").ToListAsync();
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
            var data = await _dbContext.RewardMaster
                .Where(x => (MemberId != "ALL") ? x.MemberId == MemberId : x.MemberId != null)
                .ToListAsync();
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


            if (_dbContext.StoreMaster.Count(x => x.StoreId == obj.StoreId) == 0)
            {
                response.message = "Invalid Store Id";
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
        public async Task<IEnumerable<RewardStoreTotalDTO>> RewardStoreTotal()
        {
            var data = await (from s in _dbContext.RewardDistributor
                              group s by s.StoreId into g
                              select new RewardStoreTotalDTO
                              {
                                  storeId = g.Key,
                                  total = g.Sum(x => Convert.ToInt32(x.Level))
                              }).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<RewardDistributorInfoDTO>> RewardDistributorInfo(string storeId)
        {
            var data = await _dbContext.RewardDistributor.Where(x => x.StoreId == storeId).ToListAsync();
            return _mapper.Map<IEnumerable<RewardDistributorInfoDTO>>(data);
        }
        public async Task<string> ResetDistributorReward(string storeId)
        {
            var data = await _dbContext.RewardDistributor.Where(x => x.StoreId == storeId).ToListAsync();
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
        public async Task<IEnumerable<Plan>> GetPlan(string type)
        {
            return await _dbContext.Plan.Where(x => x.type == type).ToListAsync();
        }
        public async Task<string> AddPlan(IFormFile file, string type)
        {
            if (type == "Pdf")
            {
                var folderName = Path.Combine("Resource", "Plan");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var fullPath = Path.Combine(pathToSave, file.FileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var data = new Plan()
                {
                    file = file.FileName,
                    type = "Pdf"
                };
                var res = await _dbContext.Plan.AddAsync(data);
                await _dbContext.SaveChangesAsync();
            }
            if (type != "Pdf")
            {
                var data = new Plan()
                {
                    file = type,
                    type = "Youtube"
                };
                var res = await _dbContext.Plan.AddAsync(data);
                await _dbContext.SaveChangesAsync();
            }
            return "Success";
        }
        public async Task<string> DeletePlan(int Id)
        {
            var data = await _dbContext.Plan.Where(x => x.Id == Id).FirstOrDefaultAsync();
            _dbContext.Plan.Remove(data);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
        public async Task<TermsCondition> GetTermsCondition()
        {
            return await _dbContext.TermsCondition.FirstOrDefaultAsync();
        }
        public async Task<string> AddTerms(string content)
        {
            var terms = _dbContext.TermsCondition.ToList();
            _dbContext.TermsCondition.RemoveRange(terms);
            await _dbContext.SaveChangesAsync();

            var data = new TermsCondition() { content = content };
            var res = await _dbContext.TermsCondition.AddAsync(data);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
        public async Task<string> DeleteTerms(int id)
        {
            var data = await _dbContext.TermsCondition.Where(x => x.Id == id).FirstOrDefaultAsync();
            _dbContext.TermsCondition.Remove(data);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
        //Store Master
        public async Task<IEnumerable<StoreMaster>> GetStore()
        {
            return await _dbContext.StoreMaster.ToListAsync();
        }
        public async Task<IEnumerable<StoreMaster>> GetStore(string param)
        {
            return await _dbContext.StoreMaster.Where(x => x.State == param || x.StoreId == param || x.type == param || x.Mobile == param || x.City == param || x.PinCode == param).ToListAsync();
        }
        public async Task<string> AddStore(StoreMaster obj)
        {
            var res = string.Empty;
            if (obj.AutoId == 0)
            {
                obj.StoreId = "STR" + $"{(_dbContext.StoreMaster.Count() + 1):D7}";
                var result = await _dbContext.StoreMaster.AddAsync(obj);
                res = result.ToString();
            }
            else
            {
                var result = _dbContext.StoreMaster.Update(obj);
                res = result.ToString();
            }
            await _dbContext.SaveChangesAsync();
            return res;
        }
        public async Task<string> DeleteStore(int Id)
        {
            var store = _dbContext.StoreMaster.Where(x => x.AutoId == Id).FirstOrDefault();
            _dbContext.StoreMaster.Remove(store);
            var result = await _dbContext.SaveChangesAsync();
            return result.ToString();
        }
        public async Task<string> AddKyc(KycDocumentDTO dto)
        {
            //var fileName = dto.memberId + '_' + dto.type + Path.GetExtension(dto.file.FileName);
            //FileUpload.upload(dto.file, fileName);

            var data = new KycDocument()
            {
                MemberId = dto.memberId,
                file = "-",
                type = dto.type
            };
            var res = await _dbContext.KycDocument.AddAsync(data);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
        public async Task<IEnumerable<KycDocument>> GetKyc(string memberId)
        {
            return await _dbContext.KycDocument.Where(x => x.MemberId == memberId).ToListAsync();
        }
        public async Task<IEnumerable<KycDocument>> GetSubscribe()
        {
            var data = (from kd in _dbContext.KycDocument
                        join mem in _dbContext.Members on kd.MemberId equals mem.MemberId
                        where (kd.type.ToLower() == "subscribe" && mem.IsSubscribe == 'N')
                        select new KycDocument()
                        {
                            MemberId = kd.MemberId,
                            file = kd.file,
                            type = kd.type
                        }).ToListAsync();
            return await data;
        }
        public void DeleteKyc(int AutoId)
        {
            var kyc = _dbContext.KycDocument.Where(x => x.AutoId == AutoId).FirstOrDefault();
            _dbContext.KycDocument.Remove(kyc);
            _dbContext.SaveChanges();

        }
    }
}
