using Microsoft.AspNetCore.Mvc;

namespace RiseConsulting.Directory.ReportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}