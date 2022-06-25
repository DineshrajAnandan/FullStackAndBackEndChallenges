using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC_GMS_API.Contracts;
using POC_GMS_API.Models.DB;
using POC_GMS_API.Shared;
using System.Threading.Tasks;

namespace POC_GMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GrantsController : ControllerBase
    {
        private readonly IGrantsService _grantsService;

        public GrantsController(IGrantsService grantsService)
        {
            _grantsService = grantsService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _grantsService.GetAllGrantsAsync());
        }

        [HttpGet("Applicant/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                return Ok(await _grantsService.GetAllGrantsAndStatusAsync(userId));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Add")]
        public async Task<IActionResult> Post([FromBody] GrantProgram grant)
        {
            try
            {
                var result = await _grantsService.AddGrantAsync(grant);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("Edit")]
        public async Task<IActionResult> Put([FromBody] GrantProgram grant)
        {
            try
            {
                await _grantsService.EditGrantAsync(grant);
                return Ok("Grant edited succesfully");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _grantsService.DeleteGrantAsync(id);
                return Ok("Grant deleted succesfully");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
