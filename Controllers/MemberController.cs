using Fingers10.ExcelExport.ActionResults;
using MadhurAPI.Models;
using MadhurAPI.Models.DTO;
using MadhurAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MadhurAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberRepository _repository;

        public MemberController(IMemberRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("GetBanner")]
        public async Task<IActionResult> GetBanner()
        {
            return Ok(await _repository.GetBanner());
        }
        [HttpPost("GenerateKey")]
        public async Task<IActionResult> GenerateKey([FromBody]int NoOfKey)
        {
            var data = await _repository.GenerateKey(NoOfKey);
            return Ok(data);
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
        [HttpGet("TotalKey")]
        public async Task<IActionResult> TotalKey()
        {
            var data = await _repository.TotalKey();
            return Ok(data);
        }
        [HttpPost("GetMembers")]
        public async Task<IActionResult> GetMembers([FromBody] FilterDTO obj)
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
        [HttpGet("LevelReport")]
        public async Task<IActionResult> LevelReport()
        {
            var result = await _repository.LevelReport();
            return Ok(result);
        }
        [HttpGet("LevelWiseMember")]
        public async Task<IActionResult> LevelWiseMember(string level)
        {
            var result = await _repository.LevelWiseMember(level);
            return Ok(result);
        }
        [HttpGet("AllSelfMember")]
        public async Task<IActionResult> AllSelfMember(string MemberId)
        {
            var result = await _repository.AllSelfMember(MemberId);
            return Ok(result);
        }
        [HttpGet("AllSelfMemberAdmin")]
        public async Task<IActionResult> AllSelfMemberAdmin(string MemberId)
        {
            var result = await _repository.AllSelfMemberAdmin(MemberId);
            return Ok(result);
        }
        [HttpGet("TodayMember")]
        public async Task<IActionResult> TodayMember(string MemberId)
        {
            var result = await _repository.TodayMember(MemberId);
            return Ok(result);
        }
        [HttpGet("GetRepurchase")]
        public async Task<IActionResult> GetRepurchase(string MemberId)
        {
            var result = await _repository.GetRepurchase(MemberId);
            return Ok(result);
        }
        [HttpGet("TotalRepurchase")]
        public async Task<IActionResult> TotalRepurchase()
        {
            var result = await _repository.TotalRepurchase();
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

        [HttpGet("RegKeys")]
        public async Task<IActionResult> RegKeys(char param)
        {
            return Ok(await _repository.RegKeys(param));
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

        [HttpPost("Repurchase")]
        public async Task<IActionResult> Repurchase(string memberId, string RegKey)
        {
            Response result = await _repository.Repurchase(memberId, RegKey);
            return Ok(result);
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
        public async Task<IActionResult> UpdateRegKeys([FromBody] string[] keys)
        {
            await _repository.UpdateRegKeys(keys);
            return Ok();
        }
        [HttpPost("AddReward")]
        public async Task<IActionResult> AddReward([FromForm] RewardMasterDTO obj)
        {
            if (obj.file_path == null && obj.file_path.Length == 0)
            {
                return BadRequest("Invalid File !");
            }
            var result = await _repository.AddReward(obj);
            return Ok(result);
        }

        [HttpGet("GetReward")]
        public async Task<IActionResult> GetReward(string MemberId)
        {
            var result = await _repository.GetReward(MemberId);
            return Ok(result);
        }

        [HttpPost("ApproveReward")]
        public async Task<IActionResult> ApproveReward([FromBody] RewardDistributorDTO obj)
        {
            var result = await _repository.ApproveReward(obj);
            return Ok(result);
        }
        [HttpGet("RewardDistributorInfo")]
        public async Task<IActionResult> RewardDistributorInfo(string distributorId)
        {
            var result = await _repository.RewardDistributorInfo(distributorId);
            return Ok(result);
        }
        [HttpDelete("ResetDistributorReward")]
        public async Task<IActionResult> ResetDistributorReward(string distributorId)
        {
            var result = await _repository.ResetDistributorReward(distributorId);
            return Ok(result);
        }

        [HttpDelete("DeleteReward")]
        public async Task<IActionResult> DeleteReward(int AutoId)
        {
            var result = await _repository.DeleteReward(AutoId);
            return Ok(result);
        }
        [HttpPut("EditReward")]
        public async Task<IActionResult> EditReward(int AutoId, string Remark)
        {
            var result = await _repository.EditReward(AutoId, Remark);
            return Ok(result);
        }
        [HttpGet("GetPlan")]
        public async Task<IActionResult> GetPlan(string type)
        {
            var result = await _repository.GetPlan(type);
            return Ok(result);
        }
        [HttpPost("AddPlan")]
        public async Task<IActionResult> AddPlan([FromForm] UploadPlanDTO obj)
        {
            var result = await _repository.AddPlan(obj.file, obj.type);
            return Ok(result);
        }
        [HttpDelete("DeletePlan")]
        public async Task<IActionResult> DeletePlan(int Id)
        {
            var result = await _repository.DeletePlan(Id);
            return Ok(result);
        }
        [HttpGet("GetTermsCondition")]
        public async Task<IActionResult> GetTermsCondition()
        {
            var result = await _repository.GetTermsCondition();
            return Ok(result);
        }
        [HttpPost("AddTerms")]
        public async Task<IActionResult> AddTerms([FromBody]TermsCondition obj)
        {
            var result = await _repository.AddTerms(obj.content);
            return Ok(result);
        }
        [HttpDelete("DeleteTerms")]
        public async Task<IActionResult> DeleteTerms(int id)
        {
            var result = await _repository.DeleteTerms(id);
            return Ok(result);
        }
        //Store Master
        [HttpGet("GetStore")]
        public async Task<IActionResult> GetStore()
        {
            var result = await _repository.GetStore();
            return Ok(result);
        }
        [HttpGet("GetStore/{state}/{city}")]
        public async Task<IActionResult> GetStore(string state, string city)
        {
            var result = await _repository.GetStore(state, city);
            return Ok(result);
        }
        [HttpPost("AddStore")]
        public async Task<IActionResult> AddStore([FromBody] StoreMaster obj)
        {
            var result = await _repository.AddStore(obj);
            return Ok(result);
        }
        [HttpDelete("DeleteStore/{id:int}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _repository.DeleteStore(id);
            return Ok(result);
        }
    }
}
