using MadhurAPI.Models;
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
        Task<IEnumerable<YoutubeVideo>> GetVideo();
        Task<string> AddVideo(string code);
        Task<string> DeleteVideo(string code);
        Task<TermsCondition> GetTermsCondition();
        Task<string> AddTerms(string content);
        Task<string> DeleteTerms(int id);
    }
}
