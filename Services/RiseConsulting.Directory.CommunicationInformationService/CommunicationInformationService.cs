using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CommunicationInformationService
{
    public class CommunicationInformationService : ICommunicationInformationService
    {
        private readonly IGenericRepository<CommunicationInformation> _communicationInformationRepository;

        public CommunicationInformationService(IGenericRepository<CommunicationInformation> communicationInformationRepository)
        {
            _communicationInformationRepository = communicationInformationRepository;
        }

        public CommunicationInformation AddCommunicationInformation(CommunicationInformation obj)
        {
            _communicationInformationRepository.Insert(obj);

            _communicationInformationRepository.SaveChanges();

            return obj;
        }

        public async Task<CommunicationInformation> AddCommunicationInformationAsync(CommunicationInformation obj)
        {
            await _communicationInformationRepository.InsertAsync(obj);

            await _communicationInformationRepository.SaveChangesAsync();

            return obj;
        }

        public void DeleteCommunicationInformation(object id)
        {
            _communicationInformationRepository.Delete(id);

            _communicationInformationRepository.SaveChanges();
        }

        public async Task DeleteCommunicationInformationAsync(object id)
        {
            await _communicationInformationRepository.DeleteAsync(id);

            await _communicationInformationRepository.SaveChangesAsync();
        }

        public List<CommunicationInformation> GetAllCommunicationInformations()
        {
            return _communicationInformationRepository.GetAll();
        }

        public async Task<List<CommunicationInformation>> GetAllCommunicationInformationsAsync()
        {
            return await _communicationInformationRepository.GetAllAsync();
        }

        public List<CommunicationInformation> GetAllCommunicationInformationsWithCriteria(Expression<Func<CommunicationInformation, bool>> filterExpression)
        {
            return _communicationInformationRepository.GetAllWithCriteria(filterExpression);
        }

        public async Task<List<CommunicationInformation>> GetAllCommunicationInformationsWithCriteriaAsync(Expression<Func<CommunicationInformation, bool>> filterExpression)
        {
            return await _communicationInformationRepository.GetAllWithCriteriaAsync(filterExpression);
        }

        public CommunicationInformation GetCommunicationInformationById(object id)
        {
            return _communicationInformationRepository.GetById(id);
        }

        public async Task<CommunicationInformation> GetCommunicationInformationByIdAsync(object id)
        {
            return await _communicationInformationRepository.GetByIdAsync(id);
        }

        public CommunicationInformation GetCommunicationInformationWithCriteria(Expression<Func<CommunicationInformation, bool>> filterExpression)
        {
            return _communicationInformationRepository.GetWithCriteria(filterExpression);
        }

        public async Task<CommunicationInformation> GetCommunicationInformationWithCriteriaAsync(Expression<Func<CommunicationInformation, bool>> filterExpression)
        {
            return await _communicationInformationRepository.GetWithCriteriaAsync(filterExpression);
        }

        public void UpdateCommunicationInformation(CommunicationInformation obj)
        {
            _communicationInformationRepository.Update(obj);

            _communicationInformationRepository.SaveChanges();
        }
    }
}