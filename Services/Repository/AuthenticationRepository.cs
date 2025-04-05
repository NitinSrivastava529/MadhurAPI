using AutoMapper;
using Azure;
using MadhurAPI.Data;
using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using MadhurAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Response = MadhurAPI.Models.Response;

namespace MadhurAPI.Services.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _dbContext; private readonly IMapper _mapper;
        public AuthenticationRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
        public async Task<RegistrationDTO> AddMember(Member member)
        {
            var response = new RegistrationDTO();
            if (_dbContext.RegKeys.Count(x => x.Key == member.RegPin) == 0)
            {
                response.Message = "Registration Pin Not Valid";
                return response;
            }
            if (_dbContext.StoreMaster.Count(x => x.StoreId == member.StoreId) == 0)
            {
                response.Message = "StoreId Not Valid";
                return response;
            }
            if (_dbContext.Members.Count(x => x.RegPin == member.RegPin) > 0)
            {
                response.Message = "This Registration Pin Already Used";
                return response;
            }

            if (_dbContext.Members.Count(x => x.MobileNo == member.MobileNo) > 0)
            {
                response.Message = "Mobile No Already Exists";
                return response;
            }

            //if (_dbContext.Members.Count(x => x.AadharNo == member.AadharNo) > 0)
            //{
            //    response.Message = "AadharNo No Already Exists";
            //    return response;
            //}

            member.MemberId = "MAC" + $"{(_dbContext.Members.Count() + 1):D10}";

            var result = _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            if (result.IsCompleted)
            {
                var storeKeyInfo = new StorekeyInfo()
                {
                    StoreId = member.StoreId,
                    MemberId = member.MemberId
                };
                _dbContext.StorekeyInfo.Add(storeKeyInfo);
                var dataKey = await _dbContext.RegKeys.FirstOrDefaultAsync(x => x.Key == member.RegPin);
                if (dataKey != null)
                    _dbContext.RegKeys.Remove(dataKey);

                await _dbContext.SaveChangesAsync();
            }
            response.MemberId = member.MemberId;
            response.Password = member.Password;
            response.Message = "Congratulations !Successfully Registered.\n Login Id : " + member.MemberId + " \n Password : " + member.Password;
            return response;
        }
        public async Task<IEnumerable<StateMaster>> GetState()
        {
            var state = await _dbContext.StateMaster.ToListAsync();
            return state;
        }
        public async Task<IEnumerable<DistrictMaster>> GetDistrict(int state_code)
        {
            var district = await _dbContext.DistrictMaster.Where(x => x.state_code == state_code).ToListAsync();
            return district;
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
