using Azure;
using MadhurAPI.Data;
using MadhurAPI.Models.DTO;
using MadhurAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Response = MadhurAPI.Models.Response;

namespace MadhurAPI.Services.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _dbContext;
        public AuthenticationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response> Login(LoginDTO obj)
        {
            var response = new Response();
            if (_dbContext.Members.Count(x => x.MemberId == obj.MemberId && x.Password == obj.Password) == 0)
            {
                response.message = "MemberId or Password is Incorrect.";
                response.result = false;
                return response;
            }
            if (_dbContext.Members.Count(x => x.MemberId == obj.MemberId && x.IsActive == 'Y') == 0)
            {
                response.message = "Member is blocked by Admin.";
                response.result = false;
                return response;
            }
            if (_dbContext.Members.Count(x => x.MemberId == obj.MemberId && x.Password == obj.Password && x.IsActive == 'Y') > 0)
            {
                response.message = "Login Successful.";
                response.result = true;
            }
            return response;
        }
        public async Task<ForgetPasswordDTO> ForgetPassword(string MobileNo, string dob)
        {
            var response = new ForgetPasswordDTO();
            if (_dbContext.Members.Count(x => x.MobileNo == MobileNo && x.dob == Convert.ToDateTime(dob)) == 0)
            {
                response.MemberId = "Failed : Mobile No or Password is Incorrect.";
                return response;
            }
            if (_dbContext.Members.Count(x => x.MobileNo == MobileNo && x.dob == Convert.ToDateTime(dob)) == 0)
            {
                response.MemberId = "Failed : Member is blocked by Admin.";
                return response;
            }

            var data = await _dbContext.Members.FirstOrDefaultAsync(x => x.MobileNo == MobileNo && x.dob == Convert.ToDateTime(dob));         

            response.MemberId = data.MemberId;
            response.Password = data.Password;
            return response;
        }
        public async Task<Response> ChangePassword(ChangePasswordDTO obj)
        {
            var result = await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberId == obj.MemberId);
            if (result != null)
            {
                result.Password = obj.Password;
                await _dbContext.SaveChangesAsync();
            }
            var response = new Response()
            {
                message = "Password Changed Successfully",
                result = true
            };
            return response;
        }
    }
}
