using BrokerServices;
using Identity.Commands;
using Identity.Domain.Repos;
using Identity.Dtos;
using Identity.Events;
using System;
using System.Threading.Tasks;

namespace Identity.Domain.CommandHandlers.Implementations
{
    public class LoginCommandHandler : ILoginCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMessageProducer _messageProducer;

        public LoginCommandHandler(IApplicationUserRepository applicationUserRepository, IMessageProducer messageProducer)
        {
            _applicationUserRepository = applicationUserRepository;
            _messageProducer = messageProducer;
        }
        
        public async Task<LoginCommandResponseDto> HandleCommand(LoginCommand cmd)
        {
            string loginFailureReason = "";
            var user = _applicationUserRepository.GetActiveUserByUsername(cmd.Username);

            //this username is no good...
            if (user == null)
            {
                loginFailureReason = "User doesn't exist in our records";
                return new LoginCommandResponseDto { LoginSuccess = false, FailureReason = loginFailureReason };
            }

            var response = user.Authenticates(cmd.Password, out loginFailureReason);
            _applicationUserRepository.Save();

            if (!response)
                return new LoginCommandResponseDto { LoginSuccess = false, FailureReason = loginFailureReason };

            //fire event here...
            await _messageProducer.ProduceEventAsync<UserLoggedInEvent>(new UserLoggedInEvent
            {
                CorrelationId = (cmd.CommandId == null) ? Guid.NewGuid() : (Guid)cmd.CommandId,
                EntityId = user.Id,
                UserName = user.UserName,
                TimeStamp = user.LastLogin
            },
            typeof(UserLoggedInEvent).FullName);

            return new LoginCommandResponseDto { LoginSuccess = true, IdentityUserId = user.Id };
        }

        public void Dispose()
        {
            _applicationUserRepository.Dispose();
        }

    }
}
