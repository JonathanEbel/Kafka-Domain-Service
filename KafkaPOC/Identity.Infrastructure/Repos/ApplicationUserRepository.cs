using Core.Pagination;
using Identity.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Identity.Domain.Repos;

namespace Identity.Infrastructure.Repos
{
    public class ApplicationUserRepository : IApplicationUserRepository, IDisposable
    {
        private readonly IdentityContext _dbContext;

        public ApplicationUserRepository(IdentityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();  //we never want to delete from this repo...
        }

        public PagedResult<ApplicationUser> FindByUserName(string text, QueryConstraints<ApplicationUser> constraints)
        {
            var query = _dbContext.ApplicationUsers.Where(x => x.UserName.StartsWith(text));
            var count = query.Count();
            var items = constraints.ApplyTo(query).ToList();

            return new PagedResult<ApplicationUser>(items, count);
        }

        public PagedResult<ApplicationUser> FindAll(QueryConstraints<ApplicationUser> constraints)
        {
            var items = constraints.ApplyTo(_dbContext.ApplicationUsers).ToList();
            var count = items.Count;

            return new PagedResult<ApplicationUser>(items, count);
        }

        public void Add(ApplicationUser entity)
        {
            _dbContext.ApplicationUsers.Add(entity);
        }

        public ApplicationUser Get(Guid id)
        {
            return _dbContext.ApplicationUsers.Where(x => x.Id == id).FirstOrDefault();
        }

        public ApplicationUser Get(string userName)
        {
            return _dbContext.ApplicationUsers.Include(x => x.Claims).Where(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
        }

        public ApplicationUser GetById(Guid id)
        {
            return _dbContext.ApplicationUsers.Include(x => x.Claims).Where(x => x.Id == id).FirstOrDefault();
        }

        public bool UserAuthenticates(string userName, string password, out string reason)
        {
            reason = "";
            var user = _dbContext.ApplicationUsers.Where(x => x.UserName.ToLower() == userName.ToLower() && x.Active == true).FirstOrDefault();

            //this username is no good...
            if (user == null)
            {
                reason = "User doesn't exist in our records";
                return false;
            }

            var response = user.Authenticates(password, out reason);
            Save();

            return response;
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
