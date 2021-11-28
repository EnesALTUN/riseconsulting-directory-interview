using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.Core.ReportModel;
using RiseConsulting.Directory.ReportService.Infrastructure;
using System.Collections.Generic;

namespace RiseConsulting.Directory.ReportApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult GetSortByLocation()
        {
            List<ReportReturn> reportReturn = _reportService.GetSortByLocation();

            return Ok(reportReturn);
        }

        [HttpGet("{location}")]
        public IActionResult GetUserCountByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
                return BadRequest();

            ReportReturn result = _reportService.GetUserCountByLocation(location);

            if (result is null)
                return NoContent();

            return Ok(result);
        }
    }
}