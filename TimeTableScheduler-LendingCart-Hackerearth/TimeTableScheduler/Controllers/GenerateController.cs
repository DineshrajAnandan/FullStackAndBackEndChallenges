using Microsoft.AspNetCore.Mvc;
using System.Text;
using TimeTableScheduler.Common.Constants;
using TimeTableScheduler.Common.Extensions;
using TimeTableScheduler.Common.Models;
using TimeTableScheduler.Common.Services.Contracts;

namespace TimeTableScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly IClassService _classService;

        public GenerateController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var classes = _classService.GetClasses();
            var sb = new StringBuilder();
            sb.AppendLine(Constants.TimeTableCsvHeader);
            byte[] bytes = BuildCsv(classes, sb);
            return File(bytes, Constants.csvDownloadMimeType, Constants.csvDownloadName);
        }

        private static byte[] BuildCsv(IEnumerable<ClassViewModel> classes, StringBuilder sb)
        {
            foreach (var clazz in classes)
            {
                for (int i = 0; i < 5; i++)
                {
                    var record = $"{clazz.Number}, {Constants.WorkingDays[i]}, {string.Join(", ", clazz.Subjects.GetRangeOfItemsFromIndex(i, 4))}";
                    sb.AppendLine(record);
                }
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}