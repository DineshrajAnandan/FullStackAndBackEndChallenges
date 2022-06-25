using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC_GMS_API.Contracts;
using POC_GMS_API.Models.DTO;
using System;
using System.Threading.Tasks;

namespace POC_GMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var result = await _applicantService.GetApplicantAsync(userId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ApplicantInfo applicantInfo)
        {
            try
            {
                await _applicantService.UpdateApplicantAsync(applicantInfo);
                return Ok("Applicant details updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
