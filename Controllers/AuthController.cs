using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using MadhurAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MadhurAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationRepository _repository;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthenticationRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO obj)
        {
            Response response = await _repository.Login(obj);
            if (response.result)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("MemberId",obj.MemberId.ToString()),
                    new Claim("CreationDate",DateTime.UtcNow.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokens = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                    );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(tokens);
                return Ok(new { Token = tokenValue, response });
            }
            return Ok(new { Token = "", response });
        }
        [HttpGet("GetState")]
        public async Task<IActionResult> GetState()
        {
            try
            {
                var result = await _repository.GetState();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetDistrict/{StateCode}")]
        public async Task<IActionResult> GetDistrict(int StateCode)
        {
            try
            {
                var result = await _repository.GetDistrict(StateCode);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("ChangePassword")]
        public async Task<Response> ChangePassword([FromBody] ChangePasswordDTO obj)
        {
            var response = new Response();
            try
            {
                response = await _repository.ChangePassword(obj);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.result = false;
            }
            return response;
        }
        [HttpGet("ForgetPassword/{MobileNo}/{dob}")]
        public async Task<ForgetPasswordDTO> ForgetPassword(string MobileNo, string dob)
        {
            ForgetPasswordDTO response = await _repository.ForgetPassword(MobileNo, dob);
            return response;
        }      
        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromBody] Member member)
        {
            try
            {
                RegistrationDTO result = await _repository.AddMember(member);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
