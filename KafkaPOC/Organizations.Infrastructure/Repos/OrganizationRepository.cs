using Organizations.Domain.Models;
using Organizations.Domain.Repos;
using System;
using System.Linq;

namespace Organizations.Infrastructure.Repos
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly OrganizationsContext _dbContext;

        public OrganizationRepository(OrganizationsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Organization entity)
        {
            _dbContext.Organizations.Add(entity);
        }

        public void Delete(Organization entity)
        {
            throw new NotImplementedException();
        }
        
        public Organization Get(Guid id)
        {
            return _dbContext.Organizations.FirstOrDefault(x => x.ID == id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
