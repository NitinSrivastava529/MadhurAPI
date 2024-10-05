using MadhurAPI.Models;
using MadhurAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadhurAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberRepository _repository;

        public MemberController(IMemberRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetMembers")]
        public async Task<IActionResult> GetMembers()
        {
            var result = await _repository.GetMembers();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetMember/{id}")]
        public async Task<IActionResult> GetMember(string memberId)
        {
            var result = await _repository.GetMember(memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromBody] Member member)
        {
            var result = await _repository.AddMember(member);
            return Ok(result);
        }
        [HttpPut("UpdateMember")]
        public async Task<IActionResult> UpdateMember([FromBody] Member member)
        {
            var result = await _repository.UpdateMember(member);
            return Ok(result);
        }
        [HttpDelete("DeleteMember")]
        public void DeleteMember(string memberId)
        {
            _repository.DeleteMember(memberId);
        }
    }
}
