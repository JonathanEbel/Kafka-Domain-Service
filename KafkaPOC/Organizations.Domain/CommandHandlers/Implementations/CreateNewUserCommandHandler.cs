using System;
using System.Threading.Tasks;
using BrokerServices;
using Organizations.Commands;
using Organizations.Domain.Repos;
using Organizations.Events;

namespace Organizations.Domain.CommandHandlers.Implementations
{
    public class CreateNewUserCommandHandler : ICreateNewUserCommandHandler
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMessageProducer _messageProducer;

        public CreateNewUserCommandHandler(IOrganizationRepository organizationRepository, IMessageProducer messageProducer)
        {
            _organizationRepository = organizationRepository;
            _messageProducer = messageProducer;
        }

        public async Task<Guid> HandleCommand(CreateNewUserCommand cmd)
        {
            var org = _organizationRepository.Get(cmd.OrganizationId);
            if (org == null)
                throw new Exception("Organization does not exist.");

            var user = org.AddNewUser(cmd.FirstName, cmd.LastName, cmd.Email, cmd.PhoneNumber, cmd.IdentityId, active: true);
            _organizationRepository.Save();

            await _messageProducer.ProduceEventAsync<UserCreatedEvent>(new UserCreatedEvent {
                CorrelationId = (cmd.CommandId == null) ? Guid.NewGuid() : (Guid)cmd.CommandId,
                Active = user.Active,
                DateJoined = user.DateJoined,
                Email = user.Email,
                EntityId = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IdentityId = user.IdentityId,
                OrganizationId = org.ID,
                PhoneNumber = user.PhoneNumber,
                TimeStamp = DateTime.UtcNow
            });

            return user.ID;

        }
    }
}
