using BackgroundProcessor.Contracts;
using BackgroundProcessor.Models;
using BackgroundProcessor.Processor;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackgroundProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IBatchProcessor _batchProcessor;
        private readonly IDbAccessService _dbAccessService;

        public ProcessController(IBatchProcessor batchProcessor,
            IDbAccessService dbAccessService)
        {
            _batchProcessor = batchProcessor;
            _dbAccessService = dbAccessService;
        }

        [HttpPost("start")]
        public IActionResult StartProcess([FromBody] StartProcessRequest request)
        {
            try
            {
                if (BatchProcessor.IsProcessRunning)
                {
                    return StatusCode(405, "Process already running");
                }
                BackgroundJob.Enqueue(() => 
                    _batchProcessor.StartProcess(request.BatchesNos, request.NosPerBatch));
                return Ok("Process started.");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("CurrentProcessStatus")]
        public IActionResult CurrentProcessStatus()
        {
            try
            {
                var result = _dbAccessService.GetCurrentBatches();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("PreviousProcessStatus")]
        public IActionResult PreviousProcessStatus()
        {
            try
            {
                var result = _dbAccessService.GetPreviousBatches();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
