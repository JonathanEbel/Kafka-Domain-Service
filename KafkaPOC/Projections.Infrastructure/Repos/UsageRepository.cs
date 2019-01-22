using Projections.Domain.Models;
using Projections.Domain.Repos;
using System;
using System.Linq;

namespace Projections.Infrastructure.Repos
{
    public class UsageRepository : IUsageRepository
    {
        private readonly ProjectionsContext _dbContext;

        public UsageRepository(ProjectionsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Usage GetByIdentityId(Guid id)
        {
            return _dbContext.Usages.FirstOrDefault(x => x.IdentityUserId == id);
        }

        public void Add(Usage entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(Usage entity)
        {
            throw new NotImplementedException();
        }
        
        public Usage Get(int id)
        {
            return _dbContext.Usages.FirstOrDefault(x => x.ID == id);
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
