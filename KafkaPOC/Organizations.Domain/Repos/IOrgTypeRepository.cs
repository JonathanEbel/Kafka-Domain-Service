using Core;
using Organizations.Domain.Models;
using System;

namespace Organizations.Domain.Repos
{
    public interface IOrgTypeRepository : IRepository<OrgType, int>, IDisposable
    {

    }
}
