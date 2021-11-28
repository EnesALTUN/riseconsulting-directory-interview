using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Core.ReportModel;
using RiseConsulting.Directory.ReportService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.ReportApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IDistributedCache _redisDistributedCache;

        private readonly DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));

        public ReportController(IReportService reportService, IDistributedCache distributedCache)
        {
            _reportService = reportService;
            _redisDistributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetSortByLocation()
        {
            string cacheJsonItem;
            List<ReportReturn> reportReturns;

            var sortByLocationCache = await _redisDistributedCache.GetAsync("GetSortByLocation");

            if (sortByLocationCache is null)
            {
                reportReturns = _reportService.GetSortByLocation();

                cacheJsonItem = JsonConvert.SerializeObject(reportReturns);

                sortByLocationCache = Encoding.UTF8.GetBytes(cacheJsonItem);

                await _redisDistributedCache.SetAsync("GetSortByLocation", sortByLocationCache, options);
            }
            else
            {
                cacheJsonItem = Encoding.UTF8.GetString(sortByLocationCache);
                reportReturns = JsonConvert.DeserializeObject<List<ReportReturn>>(cacheJsonItem);
            }

            return Ok(new ApiReturn<List<ReportReturn>>{ Success = true, Code = StatusCodes.Status200OK, Data = reportReturns});
        }

        [HttpGet("{location}")]
        public async Task<IActionResult> GetUserCountByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
                return BadRequest();

            string cacheJsonItem;
            string redisKey = $"GetUserCountByLocation{location}";
            ReportReturn result;

            var userCountByLocationCache = await _redisDistributedCache.GetAsync(redisKey);

            if (userCountByLocationCache is null)
            {
                result = _reportService.GetUserCountByLocation(location);

                if (result is null)
                    return NoContent();

                cacheJsonItem = JsonConvert.SerializeObject(result);

                userCountByLocationCache = Encoding.UTF8.GetBytes(cacheJsonItem);

                await _redisDistributedCache.SetAsync(redisKey, userCountByLocationCache, options);
            }
            else
            {
                cacheJsonItem = Encoding.UTF8.GetString(userCountByLocationCache);
                result = JsonConvert.DeserializeObject<ReportReturn>(cacheJsonItem);
            }

            return Ok(new ApiReturn<ReportReturn> { Success = true, Code = StatusCodes.Status200OK, Data = result });
        }

        [HttpGet("{location}")]
        public async Task<IActionResult> GetPhoneNumberCountByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
                return BadRequest();

            string cacheJsonItem;
            string redisKey = $"GetPhoneNumberCountByLocation{location}";
            ReportReturn result;

            var phoneNumberCountByLocationCache = await _redisDistributedCache.GetAsync(redisKey);

            if (phoneNumberCountByLocationCache is null)
            {
                result = _reportService.GetPhoneNumberCountByLocation(location);

                if (result is null)
                    return NoContent();

                cacheJsonItem = JsonConvert.SerializeObject(result);

                phoneNumberCountByLocationCache = Encoding.UTF8.GetBytes(cacheJsonItem);

                await _redisDistributedCache.SetAsync(redisKey, phoneNumberCountByLocationCache, options);
            }
            else
            {
                cacheJsonItem = Encoding.UTF8.GetString(phoneNumberCountByLocationCache);
                result = JsonConvert.DeserializeObject<ReportReturn>(cacheJsonItem);
            }

            return Ok(new ApiReturn<ReportReturn> { Success = true, Code = StatusCodes.Status200OK, Data = result });
        }
    }
}