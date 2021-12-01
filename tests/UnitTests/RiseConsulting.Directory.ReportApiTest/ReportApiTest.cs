using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Core.ReportModel;
using RiseConsulting.Directory.ReportApi.Controllers.V1;
using RiseConsulting.Directory.ReportService.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace RiseConsulting.Directory.ReportApiTest
{
    public class ReportApiTest
    {
        private readonly ReportController _controller;
        private readonly IReportService _reportService;
        private readonly IDistributedCache _distributedCache;

        public ReportApiTest()
        {
            _reportService = new Mock<IReportService>().Object;
            _distributedCache = new Mock<IDistributedCache>().Object;

            _controller = new ReportController(_reportService, _distributedCache);
        }

        [Fact]
        public async void ToGetSortByLocation_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetSortByLocation();

            // Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Fact]
        public async void ToGetSortByLocation_ReturnAllItems()
        {
            // Act
            var result = await _controller.GetSortByLocation() as OkObjectResult;

            // Assert
            Assert.IsType<ApiReturn<List<ReportReturn>>>(result.Value);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        [InlineData("Samsun")]
        public async void ToGetUserCountByLocation_ReturnOkResult(string location)
        {
            // Act
            var result = await _controller.GetUserCountByLocation(location);

            // Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Theory]
        [InlineData(null)]
        public async void ToGetUserCountByLocation_ReturnBadRequest(string location)
        {
            // Act
            var result = await _controller.GetUserCountByLocation(location) as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("Bursa")]
        public async void ToGetUserCountByLocation_ReturnsAllItems(string location)
        {
            // Act
            var result = await _controller.GetUserCountByLocation(location) as OkObjectResult;

            // Assert
            Assert.IsType<ApiReturn<ReportReturn>>(result.Value);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        [InlineData("Samsun")]
        public async void ToGetPhoneNumberCountByLocation_ReturnOkResult(string location)
        {
            // Act
            var result = await _controller.GetPhoneNumberCountByLocation(location);

            // Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Theory]
        [InlineData(null)]
        public async void ToGetPhoneNumberCountByLocation_ReturnBadRequest(string location)
        {
            // Act
            var result = await _controller.GetPhoneNumberCountByLocation(location) as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("Bursa")]
        public async void ToGetPhoneNumberCountByLocation_ReturnsAllItems(string location)
        {
            // Act
            var result = await _controller.GetPhoneNumberCountByLocation(location) as OkObjectResult;

            // Assert
            Assert.IsType<ApiReturn<ReportReturn>>(result.Value);
        }
    }
}