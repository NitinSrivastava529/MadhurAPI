using MadhurAPI.Models;

namespace MadhurAPI.Services.Interface
{
    public interface IMemberRepository : IDisposable
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMember(string memberId);
        Task<string> AddMember(Member member);
        Task<Member> UpdateMember(Member member);
        void DeleteMember(string MemberId);
    }
}
