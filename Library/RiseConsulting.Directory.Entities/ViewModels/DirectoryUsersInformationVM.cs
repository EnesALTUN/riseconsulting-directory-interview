using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;

namespace RiseConsulting.Directory.Entities.ViewModels
{
    public class DirectoryUsersInformationVM
    {
        public Guid DirectoryUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public Guid UserId { get; set; }
        public List<CommunicationInformation> CommunicationInformations { get; set; }
    }
}