﻿using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

namespace MadhurAPI.Services.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MemberDTO>> GetMembers(FilterDTO obj);
        Task<IEnumerable<RegKey>> RegKeys(char param);
        Task<TotalKeyDTO> TotalKey();
        Task<TotalCount> TotalCount();
        Task<Member> GetMember(string memberId);
        Task<IEnumerable<Member>> GetRepurchase(string memberId);
        Task<TotalRepurchaseDTO> TotalRepurchase();
        Task<IEnumerable<MemberDTO>> GetTodayMembers();
        Task<Response> Repurchase(string MemberId, string RegKey);
        Task<Member> UpdateMember(Member member);
        Task<bool> UpdateRegKeys(string[] keys);
        Task<string> UpdateStatus(string memberId);
        Task<IEnumerable<AllMemberDTO>> AllMember(string MemberId);
        Task<IEnumerable<AllSelfMemberDTO>> AllSelfMember(string MemberId);
        Task<IEnumerable<AllSelfMemberDTO>> AllSelfMemberAdmin(string MemberId);
        Task<IEnumerable<AllMemberDTO>> TodayMember(string MemberId);
        Task<IEnumerable<LevelCount>> LevelCount(string MemberId);
        Task<IEnumerable<LevelReportDTO>> LevelReport();
        Task<IEnumerable<LevelWiseMemberDTO>> LevelWiseMember(string Prm1);
        Task<Response> AddReward(RewardMasterDTO dto);
        Task<IEnumerable<RewardMaster>> GetReward(string MemberId);
        Task<Response> GenerateKey(int NoOfKey);
        Task<IEnumerable<BannerMaster>> GetBanner();
        Task<Response> ApproveReward(RewardDistributorDTO obj);
        Task<IEnumerable<RewardDistributorInfoDTO>> RewardDistributorInfo(string distributorId);
        Task<string> ResetDistributorReward(string distributorId);
        Task<string> DeleteReward(int AutoId);
        Task<string> EditReward(int AutoId, string Remark);
        Task<IEnumerable<Plan>> GetPlan(string type);
        Task<string> AddPlan(IFormFile file, string type);
        Task<string> DeletePlan(int Id);
        Task<TermsCondition> GetTermsCondition();
        Task<string> AddTerms(string content);
        Task<string> DeleteTerms(int id);
        Task<IEnumerable<StoreMaster>> GetStore();
        Task<IEnumerable<StoreMaster>> GetStore(string state, string city);
        Task<string> AddStore(StoreMaster obj);
        Task<string> DeleteStore(int Id);
    }
}
