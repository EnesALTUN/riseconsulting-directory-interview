using RiseConsulting.Directory.Core.ReportModel;
using System.Collections.Generic;

namespace RiseConsulting.Directory.ReportService.Infrastructure
{
    public interface IReportService
    {
        List<ReportReturn> GetSortByLocation();
        ReportReturn GetUserCountByLocation(string location);
    }
}