using AutoMapper;
using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

namespace MadhurAPI.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<RewardMasterDTO, RewardMaster>()
                .ForMember(dest => dest.file_path, opt => opt.MapFrom(src => src.MemberId + "_" + src.level + "_" + src.file_path.FileName))
                .ReverseMap();
            CreateMap<RewardDistributor, RewardDistributorDTO>().ReverseMap();
            CreateMap<RewardDistributorInfoDTO,RewardDistributor>().ReverseMap();
        }
    }
}
