using Core;
using Core.Pagination;
using Identity.Models;
using System;

namespace Identity.Domain.Repos
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, Guid>, IDisposable
    {
        PagedResult<ApplicationUser> FindAll(QueryConstraints<ApplicationUser> constraints);
        PagedResult<ApplicationUser> FindByUserName(string text, QueryConstraints<ApplicationUser> constraints);
        bool UserAuthenticates(string userName, string password, out string reason);
        ApplicationUser Get(string userName);
        ApplicationUser GetById(Guid id);
    }
}
