using RiseConsulting.Directory.Core.ReportModel;
using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.ReportService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.ReportService
{
    public class ReportService : IReportService
    {
        public List<ReportReturn> GetSortByLocation()
        {
            var result = new List<ReportReturn>();

            using (RiseConsultingDirectoryDbContext db = new RiseConsultingDirectoryDbContext())
            {
                result = db.CommunicationInformation.GroupBy(n => n.Location)
                    .Select(group => new ReportReturn
                    {
                        Location = group.Key,
                        CountUsersByLocation = group.Count()

                    })
                    .OrderByDescending(order => order.CountUsersByLocation)
                    .ToList();
            }

            return result;
        }

        public ReportReturn GetUserCountByLocation(string location)
        {
            var result = new ReportReturn();

            using (RiseConsultingDirectoryDbContext db = new RiseConsultingDirectoryDbContext())
            {
                result = db.CommunicationInformation.Where(loc => loc.Location == location).GroupBy(n => n.Location)
                    .Select(group => new ReportReturn
                    {
                        Location = group.Key,
                        CountUsersByLocation = group.Count()

                    })
                    .OrderByDescending(order => order.CountUsersByLocation)
                    .FirstOrDefault();
            }

            return result;
        }
    }
}