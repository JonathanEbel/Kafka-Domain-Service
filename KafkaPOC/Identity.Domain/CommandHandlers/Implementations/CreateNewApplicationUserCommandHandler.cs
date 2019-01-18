using BrokerServices;
using Core.Constants;
using Identity.Commands;
using Identity.Domain.Repos;
using Identity.Events;
using Identity.Models;
using System;
using System.Threading.Tasks;

namespace Identity.Domain.CommandHandlers.Implementations
{
    public class CreateNewApplicationUserCommandHandler : ICreateNewApplicationUserCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMessageProducer _messageProducer;

        public CreateNewApplicationUserCommandHandler(IApplicationUserRepository applicationUserRepository, IMessageProducer messageProducer)
        {
            _applicationUserRepository = applicationUserRepository;
            _messageProducer = messageProducer;
        }

        public async Task<ApplicationUser> Handle(CreateNewApplicationUserCommand cmd, bool useStrongPassword)
        {
            if (cmd.CommandId == null)
                cmd.CommandId = Guid.NewGuid();

            var applicationUser = new ApplicationUser(cmd.UserName, cmd.Password, cmd.ConfirmPassword, useStrongPassword, true);

            foreach (var role in cmd.Roles)
                applicationUser.AddClaim(Constants.CLAIM_ROLE, role);

            _applicationUserRepository.Add(applicationUser);
            _applicationUserRepository.Save();

            //fire event here...
            await _messageProducer.ProduceEventAsync<ApplicationUserCreatedEvent>(new ApplicationUserCreatedEvent
            {
                 CorrelationId = (cmd.CommandId == null) ? Guid.NewGuid() : (Guid)cmd.CommandId,
                 EntityId = applicationUser.Id,
                 Active = applicationUser.Active,
                 Claims = cmd.Roles,
                 DateCreated = applicationUser.DateCreated,
                 UserName = applicationUser.UserName
            }, 
            typeof(ApplicationUserCreatedEvent).FullName);

            return applicationUser;
        }

    }
}
