using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            try
            {
                _logger.LogDebug($"Received compensation create request for '{compensation.employee} {compensation.salary} {compensation.effectiveDate}'");

                _compensationService.Create(compensation);

                return CreatedAtRoute("readCompensation", new { id = compensation.employee.EmployeeId }, compensation);
            }
            catch (Exception)
            {

                return null;
            }
    
        }

        // GET api/<CompensationController>/5
        [HttpGet("{id}", Name = "readCompensation")]
        public IActionResult readCompensation(string id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null || compensation.Count == 0)
                return NotFound();

            return Ok(compensation);
        }

    }
}
