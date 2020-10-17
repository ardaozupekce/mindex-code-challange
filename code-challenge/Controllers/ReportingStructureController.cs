using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        // GET api/<ReportingStructureController>/5
        [HttpGet("{id}", Name = "GetReport")]
        public IActionResult GetReport(string id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.getReportingStructure(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }


    }
}
