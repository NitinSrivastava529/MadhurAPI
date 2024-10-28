using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
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

        [HttpGet("GenerateKey")]
        public async Task<IActionResult> GenerateKey()
        {
            try
            {
                var data = await _repository.GenerateKey();
                if (data == null)
                {
                    return NotFound("Data Not Found");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("TotalCount")]
        public async Task<IActionResult> TotalCount()
        {
            try
            {
                var data = await _repository.TotalCount();
                if (data == null)
                {
                    return NotFound("Data Not Found");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
        [HttpGet("LevelCount")]
        public async Task<IActionResult> LevelCount(string MemberId)
        {
            var result = await _repository.LevelCount(MemberId);
            return Ok(result);
        }
        [HttpGet("MemberRecursive")]
        public async Task<IActionResult> MemberRecursive(string MemberId, string Logic)
        {
            var result = await _repository.MemberRecursive(MemberId, Logic);
            return Ok(result);
        }
        [HttpGet("GetTodayMembers")]
        public async Task<IActionResult> GetTodayMembers()
        {
            try
            {
                var result = await _repository.GetTodayMembers();
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
        [HttpGet("RegKeys")]
        public async Task<IActionResult> RegKeys()
        {
            try
            {
                var result = await _repository.RegKeys();
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
                RegistrationDTO result = await _repository.AddMember(member);
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
        [HttpPut("UpdateStatus/{memberid}")]
        public async Task<IActionResult> UpdateStatus(string memberid)
        {
            try
            {
                await _repository.UpdateStatus(memberid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("UpdateRegKeys")]
        public async Task<IActionResult> UpdateRegKeys([FromBody] int[] AutotId)
        {
            await _repository.UpdateRegKeys(AutotId);
            return Ok();
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