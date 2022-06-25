using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC_GMS_API.Contracts;
using POC_GMS_API.Shared;
using System;
using System.Threading.Tasks;

namespace POC_GMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly IGrantsService _grantsService;

        public ApplicationController(IApplicantService applicantService,
            IGrantsService grantsService)
        {
            _applicantService = applicantService;
            _grantsService = grantsService;
        }

        [Authorize(Roles = Role.Applicant)]
        [HttpPut("Apply/{userId}/{grantId}")]
        public async Task<IActionResult> ApplyGrant(int userId, int grantId)
        {
            try
            {
                await _applicantService.ApplyGrantAsync(userId, grantId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return Ok("Grant applied successfully");
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateStatus/{applicationId}/{statusId}")]
        public async Task<IActionResult> UpdateStatus(int applicationId, int statusId)
        {
            try
            {
                await _applicantService.UpdateApplicationStatusAsync(applicationId, statusId);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok("Status updated successfully");
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _grantsService.GetReviewScreenDetailForAdminAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
