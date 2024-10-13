using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

namespace MadhurAPI.Services.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<IEnumerable<RegKey>> RegKeys();
        Task<Member> GetMember(string memberId);
        Task<Response> Login(LoginDTO obj);
        Task<string> AddMember(Member member);
        Task<Member> UpdateMember(Member member);
        Task<string> UpdateStatus(string memberId);
        void DeleteMember(string memberId);
        Task<string> GenerateKey();
    }
}
