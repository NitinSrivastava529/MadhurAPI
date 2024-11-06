using AutoMapper;
using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

namespace MadhurAPI.Helper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
        }
    }
}
