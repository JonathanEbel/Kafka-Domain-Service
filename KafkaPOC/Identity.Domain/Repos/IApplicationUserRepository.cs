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
        ApplicationUser Get(string userName);
        ApplicationUser GetByIdWithClaims(Guid id);
        ApplicationUser GetActiveUserByUsername(string userName);
    }
}
