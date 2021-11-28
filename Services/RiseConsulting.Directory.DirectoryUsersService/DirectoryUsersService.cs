using RiseConsulting.Directory.Data;
using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Repository.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RiseConsulting.Directory.Entities.ViewModels;

namespace RiseConsulting.Directory.DirectoryUsersService
{
    public class DirectoryUsersService : IDirectoryUsersService
    {
        private readonly IGenericRepository<DirectoryUsers> _directoryUsersRepository;
        private readonly IGenericRepository<CommunicationInformation> _communicationInformationRepository;

        public DirectoryUsersService(IGenericRepository<DirectoryUsers> directoryUsersRepository, IGenericRepository<CommunicationInformation> communicationInformationRepository)
        {
            _directoryUsersRepository = directoryUsersRepository;
            _communicationInformationRepository = communicationInformationRepository;
        }

        public DirectoryUsers AddDirectoryUser(DirectoryUsers obj)
        {
            _directoryUsersRepository.Insert(obj);

            _directoryUsersRepository.SaveChanges();

            return obj;
        }

        public async Task<DirectoryUsers> AddDirectoryUserAsync(DirectoryUsers obj)
        {
            await _directoryUsersRepository.InsertAsync(obj);

            await _directoryUsersRepository.SaveChangesAsync();

            return obj;
        }

        public void DeleteDirectoryUser(object id)
        {
            _directoryUsersRepository.Delete(id);

            _directoryUsersRepository.SaveChanges();
        }

        public async Task DeleteDirectoryUserAsync(object id)
        {
            await _directoryUsersRepository.DeleteAsync(id);

            await _directoryUsersRepository.SaveChangesAsync();
        }

        public List<DirectoryUsers> GetAllDirectoryUsers()
        {
            return _directoryUsersRepository.GetAll();
        }

        public Task<List<DirectoryUsers>> GetAllDirectoryUsersAsync()
        {
            return _directoryUsersRepository.GetAllAsync();
        }

        public List<DirectoryUsers> GetAllDirectoryUsersWithCriteria(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return _directoryUsersRepository.GetAllWithCriteria(filterExpression);
        }

        public async Task<List<DirectoryUsers>> GetAllDirectoryUsersWithCriteriaAsync(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return await _directoryUsersRepository.GetAllWithCriteriaAsync(filterExpression);
        }

        public DirectoryUsers GetDirectoryUserById(object id)
        {
            return _directoryUsersRepository.GetById(id);
        }

        public async Task<DirectoryUsers> GetDirectoryUserByIdAsync(object id)
        {
            return await _directoryUsersRepository.GetByIdAsync(id);
        }

        public DirectoryUsers GetDirectoryUserWithCriteria(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return _directoryUsersRepository.GetWithCriteria(filterExpression);
        }

        public async Task<DirectoryUsers> GetDirectoryUserWithCriteriaAsync(Expression<Func<DirectoryUsers, bool>> filterExpression)
        {
            return await _directoryUsersRepository.GetWithCriteriaAsync(filterExpression);
        }

        public void UpdateDirectoryUser(DirectoryUsers obj)
        {
            _directoryUsersRepository.Update(obj);

            _directoryUsersRepository.SaveChanges();
        }

        public DirectoryUsersInformationVM GetDirectoryUsersDetail(Guid userId, Guid directoryUserId)
        {
            List<CommunicationInformation> communicationInformations = _communicationInformationRepository.GetAllWithCriteria(filter =>
                                filter.DirectoryUsersId == directoryUserId
                              );

            var result = new DirectoryUsersInformationVM();

            using (RiseConsultingDirectoryDbContext db = new RiseConsultingDirectoryDbContext())
            {
                result = (from communicationInformation in db.CommunicationInformation
                          join directoryUsers in db.DirectoryUsers
                            on communicationInformation.DirectoryUsersId equals directoryUsers.DirectoryUsersId
                          join company in db.Company
                            on directoryUsers.CompanyId equals company.CompanyId
                          where directoryUsers.DirectoryUsersId == directoryUserId && directoryUsers.UserId == userId
                          select new DirectoryUsersInformationVM { 
                              UserId = directoryUsers.UserId,
                              Name = directoryUsers.Name,
                              Surname = directoryUsers.Surname,
                              CompanyName = company.Name,
                              DirectoryUserId = directoryUsers.DirectoryUsersId,
                              CommunicationInformations = communicationInformations
                          }
                    ).FirstOrDefault();
            }

            return result;
        }
    }
}