using MadhurAPI.Models;
using MadhurAPI.Models.DTO;

namespace MadhurAPI.Services.Interface
{
    public interface IAuthenticationRepository
    {
        Task<Response> Login(LoginDTO obj);
        Task<RegistrationDTO> AddMember(Member member);
        Task<Response> ChangePassword(ChangePasswordDTO obj);
        Task<ForgetPasswordDTO> ForgetPassword(string MobileNo, string dob);
    }
}
