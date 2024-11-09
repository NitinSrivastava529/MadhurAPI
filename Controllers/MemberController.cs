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
        [HttpPost("GetMembers")]
        public async Task<IActionResult> GetMembers([FromBody]FilterDTO obj)
        {
            try
            {
                var result = await _repository.GetMembers(obj);
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
        [HttpGet("LevelCount/{MemberId}")]
        public async Task<IActionResult> LevelCount(string MemberId)
        {
            var result = await _repository.LevelCount(MemberId);
            return Ok(result);
        }

        [HttpGet("AllMember")]
        public async Task<IActionResult> AllMember(string MemberId)
        {
            var result = await _repository.AllMember(MemberId);
            return Ok(result);
        }
        [HttpGet("LevelWiseMember")]
        public async Task<IActionResult> LevelWiseMember()
        {
            var result = await _repository.LevelWiseMember();
            return Ok(result);
        }
        [HttpGet("AllSelfMember")]
        public async Task<IActionResult> AllSelfMember(string MemberId)
        {
            var result = await _repository.AllSelfMember(MemberId);
            return Ok(result);
        }
        [HttpGet("TodayMember")]
        public async Task<IActionResult> TodayMember(string MemberId)
        {
            var result = await _repository.TodayMember(MemberId);
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
