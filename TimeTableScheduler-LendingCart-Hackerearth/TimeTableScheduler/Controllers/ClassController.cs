using Microsoft.AspNetCore.Mvc;
using TimeTableScheduler.Common.Models;
using TimeTableScheduler.Common.Services.Contracts;

namespace TimeTableScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_classService.GetClasses());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = _classService.GetClass(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClassViewModel req)
        {
           var id =  await _classService.CreateClass(req);
            return Ok($"New Entry created with Id : {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _classService.DeleteClass(id);
            return success ? Ok("Successfully deleted the entry") : NotFound();
        }
    }
}