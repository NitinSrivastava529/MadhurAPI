using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using MadhurAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadhurAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationRepository _repository;
        public AuthController(IAuthenticationRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("Login")]
        public async Task<Response> Login([FromBody] LoginDTO obj)
        {
            var response = new Response();
            try
            {
                response = await _repository.Login(obj);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.result = false;
            }
            return response;
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
    }
}
