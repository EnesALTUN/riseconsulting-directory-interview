using RiseConsulting.Directory.Core.ReportModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.ReportService.Infrastructure
{
    public interface IReportService
    {
        List<ReportReturn> GetSortByLocation();
        Task<ReportReturn> GetUserCountByLocation(string location);
    }
}