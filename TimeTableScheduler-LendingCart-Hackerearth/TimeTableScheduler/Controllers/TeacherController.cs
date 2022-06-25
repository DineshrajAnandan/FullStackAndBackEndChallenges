using Microsoft.AspNetCore.Mvc;
using TimeTableScheduler.Common.Models;
using TimeTableScheduler.Common.Services.Contracts;
using TimeTableScheduler.Repository.Context;

namespace TimeTableScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IActionResult Get()
        {
           var result = _teacherService.GetTeachers();
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var teacher = _teacherService.GetTeacher(id);
            return (teacher == null) ? NotFound() : Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeacherViewModel req)
        {
            if (req.SubjectId <= 0) return BadRequest("Enter valid subject id");
            var id = _teacherService.CreateTeacher(req);
            return Ok($"New Entry created with Id : {id}");
        }
    }
}