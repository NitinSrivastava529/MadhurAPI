using MadhurAPI.Models;

namespace MadhurAPI.Services.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<IEnumerable<RegKey>> RegKeys();
        Task<Member> GetMember(string memberId);
        Task<string> AddMember(Member member);
        Task<Member> UpdateMember(Member member);
        Task<string> UpdateStatus(string memberId);
        void DeleteMember(string memberId);
        Task<string> GenerateKey();
    }
}
