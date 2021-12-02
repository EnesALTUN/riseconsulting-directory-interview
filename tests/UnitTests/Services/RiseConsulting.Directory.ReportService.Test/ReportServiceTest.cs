using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseConsulting.Directory.Core.ReportModel;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.ReportService.Infrastructure;
using RiseConsulting.Directory.Repository;
using RiseConsulting.Directory.Repository.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace RiseConsulting.Directory.ReportService.Test
{
    public class ReportServiceTest
    {
        private readonly IReportService _reportService;

        public ReportServiceTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RiseConsultingDirectoryDbContext>(options =>
                options.UseSqlServer("Server=.;Database=RiseConsulting;Trusted_Connection=True"));

            services.AddScoped(typeof(IGenericRepository<CommunicationInformation>), typeof(GenericRepository<CommunicationInformation>));
            services.AddTransient<IReportService, ReportService>();

            var serviceProvider = services.BuildServiceProvider();

            _reportService = serviceProvider.GetService<IReportService>();
        }

        [Fact]
        public void ToGetSortByLocation_Return_ReportReturns()
        {
            var results = _reportService.GetSortByLocation();

            Assert.IsType<List<ReportReturn>>(results);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        [InlineData("Ordu")]
        public void ToGetUserCountByLocation_Return_ReportReturn(string location)
        {
            var result = _reportService.GetUserCountByLocation(location);

            Assert.IsType<ReportReturn>(result);
        }

        [Theory]
        [InlineData("Ankara")]
        public void ToGetUserCountByLocation_ReturnNull_ReportReturn(string location)
        {
            var result = _reportService.GetUserCountByLocation(location);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Ýstanbul")]
        [InlineData("Ordu")]
        public void ToGetPhoneNumberCountByLocation_Return_ReportReturn(string location)
        {
            var result = _reportService.GetPhoneNumberCountByLocation(location);

            Assert.IsType<ReportReturn>(result);
        }

        [Theory]
        [InlineData("Ankara")]
        public void ToGetPhoneNumberCountByLocation_ReturnNull_ReportReturn(string location)
        {
            var result = _reportService.GetPhoneNumberCountByLocation(location);

            Assert.Null(result);
        }
    }
}