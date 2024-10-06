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
            try
            {
                var result = await _repository.GetMembers();
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

        [HttpGet("GetMember/{memberId}")]
        public async Task<IActionResult> GetMember(string memberId)
        {
            try
            {
                var result = await _repository.GetMember(memberId);
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
        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromBody] Member member)
        {
            try
            {
                var result = await _repository.AddMember(member);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
          
        }
        [HttpPut("UpdateMember")]
        public async Task<IActionResult> UpdateMember([FromBody] Member member)
        {
            try
            {
                var result = await _repository.UpdateMember(member);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
          
        }
        [HttpDelete("DeleteMember/{memberId}")]
        public async Task<IActionResult> DeleteMember(string memberId)
        {
            try
            {
                _repository.DeleteMember(memberId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}


//{
//    "autoId": 0,
//  "regPin": "MAC0001",
//  "refId": "MEM00011",
//  "memberId": "MEM0001",
//  "memberName": "Nitin Kumar",
//  "password": "123",
//  "mobileNo": "9670244590",
//  "dob": "1993-05-03",
//  "aadharNo": "834099440938",
//  "address": "Amethi",
//  "state": "Uttar Pradesh",
//  "city": "Amethi",
//  "pinCode": "229801",
//  "nominee": "Ankit",
//  "relationWithNominee": "Brother",
//  "isActive": "Y",
//  "creationDate": "2024-10-06T11:37:32.637Z"
//}