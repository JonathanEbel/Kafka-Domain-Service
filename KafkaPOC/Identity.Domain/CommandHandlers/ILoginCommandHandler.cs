using Identity.Commands;
using Identity.Dtos;
using System;
using System.Threading.Tasks;

namespace Identity.Domain.CommandHandlers
{
    public interface ILoginCommandHandler : IDisposable
    {
        Task<LoginCommandResponseDto> HandleCommand(LoginCommand cmd);
    }
}
