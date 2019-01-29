using Organizations.Domain.Models;
using Organizations.Domain.Repos;
using System;
using System.Linq;

namespace Organizations.Infrastructure.Repos
{
    public class StateProvinceRepository : IStateProvinceRepository
    {
        private readonly OrganizationsContext _dbContext;

        public StateProvinceRepository(OrganizationsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(StateProvince entity)
        {
            _dbContext.StateProvinces.Add(entity);
        }

        public void Delete(StateProvince entity)
        {
            throw new NotImplementedException();
        }
        
        public StateProvince Get(int id)
        {
            return _dbContext.StateProvinces.FirstOrDefault(x => x.ID == id);
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
