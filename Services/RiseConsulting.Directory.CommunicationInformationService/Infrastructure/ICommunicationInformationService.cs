using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CommunicationInformationService.Infrastructure
{
    public interface ICommunicationInformationService
    {
        List<CommunicationInformation> GetAllCommunicationInformations();

        Task<List<CommunicationInformation>> GetAllCommunicationInformationsAsync();

        CommunicationInformation GetCommunicationInformationById(object id);

        Task<CommunicationInformation> GetCommunicationInformationByIdAsync(object id);

        CommunicationInformation GetCommunicationInformationWithCriteria(Expression<Func<CommunicationInformation, bool>> filterExpression);

        Task<CommunicationInformation> GetCommunicationInformationWithCriteriaAsync(Expression<Func<CommunicationInformation, bool>> filterExpression);

        List<CommunicationInformation> GetAllCommunicationInformationsWithCriteria(Expression<Func<CommunicationInformation, bool>> filterExpression);

        Task<List<CommunicationInformation>> GetAllCommunicationInformationsWithCriteriaAsync(Expression<Func<CommunicationInformation, bool>> filterExpression);

        CommunicationInformation AddCommunicationInformation(CommunicationInformation obj);

        Task<CommunicationInformation> AddCommunicationInformationAsync(CommunicationInformation obj);

        void UpdateCommunicationInformation(CommunicationInformation obj);

        void DeleteCommunicationInformation(object id);

        Task DeleteCommunicationInformationAsync(object id);
    }
}