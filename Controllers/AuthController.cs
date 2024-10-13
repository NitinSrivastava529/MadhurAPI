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
        private readonly IMemberRepository _repository;
        public AuthController(IMemberRepository repository)
        {
            _repository = repository;            
        }
        public async Task<Response> Login([FromBody] LoginDTO obj)
        {
            Response response =await _repository.Login(obj);
            return response;
        }
    }
}
