using Organizations.Domain.Models;
using Organizations.Domain.Repos;
using System;
using System.Linq;

namespace Organizations.Infrastructure.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly OrganizationsContext _dbContext;

        public UserRepository(OrganizationsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }
        
        public User Get(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.ID == id);
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
