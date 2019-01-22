using Core;
using Projections.Domain.Models;
using System;

namespace Projections.Domain.Repos
{
    public interface IUsageRepository : IRepository<Usage, int>, IDisposable
    {
        Usage GetByIdentityId(Guid id);
    }
}
