using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

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
        Task<string> GenerateKey();
    }
}
