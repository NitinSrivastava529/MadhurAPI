﻿using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

namespace MadhurAPI.Services.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MemberDTO>> GetMembers(FilterDTO obj);
        Task<IEnumerable<RegKey>> RegKeys();
        Task<TotalCount> TotalCount();
        Task<Member> GetMember(string memberId);
        Task<IEnumerable<StateMaster>> GetState();
        Task<IEnumerable<MemberDTO>> GetTodayMembers();
        Task<IEnumerable<DistrictMaster>> GetDistrict(int state_code);
        Task<RegistrationDTO> AddMember(Member member);
        Task<Member> UpdateMember(Member member);
        Task<bool> UpdateRegKeys(int[] AutoId);
        Task<string> UpdateStatus(string memberId);
        Task<IEnumerable<AllMemberDTO>> AllMember(string MemberId);
        Task<IEnumerable<AllSelfMemberDTO>> AllSelfMember(string MemberId);
        Task<IEnumerable<AllMemberDTO>> TodayMember(string MemberId);
        Task<IEnumerable<LevelCount>> LevelCount(string MemberId);
        Task<IEnumerable<LevelReportDTO>> LevelReport();
        Task<IEnumerable<LevelWiseMemberDTO>> LevelWiseMember(string Prm1);
        Task<Response> GenerateKey();       
 
    }
}
