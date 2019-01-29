using Organizations.Domain.Models;
using Organizations.Domain.Repos;
using System;
using System.Linq;

namespace Organizations.Infrastructure.Repos
{
    public class OrgTypeRepository : IOrgTypeRepository
    {
        private readonly OrganizationsContext _dbContext;

        public OrgTypeRepository(OrganizationsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(OrgType entity)
        {
            _dbContext.OrgTypes.Add(entity);
        }

        public void Delete(OrgType entity)
        {
            throw new NotImplementedException();
        }
        
        public OrgType Get(int id)
        {
            return _dbContext.OrgTypes.FirstOrDefault(x => x.ID == id);
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
