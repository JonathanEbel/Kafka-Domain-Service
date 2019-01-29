using Core;
using Organizations.Domain.Models;
using System;

namespace Organizations.Domain.Repos
{
    public interface IOrganizationRepository : IRepository<Organization, Guid>, IDisposable
    {
        
    }
}
