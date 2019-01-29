using BrokerServices;
using Organizations.Commands;
using Organizations.Domain.Models;
using Organizations.Domain.Repos;
using Organizations.Dtos;
using Organizations.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organizations.Domain.CommandHandlers.Implementations
{
    public class AddOrganizationCommandHandler : IAddOrganizationCommandHandler
    {
        private readonly IStateProvinceRepository _stateProvinceRepository;
        private readonly IOrgTypeRepository _orgTypeRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMessageProducer _messageProducer;

        public AddOrganizationCommandHandler(IStateProvinceRepository stateProvinceRepository, IOrgTypeRepository orgTypeRepository,
                                    IOrganizationRepository organizationRepository, IMessageProducer messageProducer)
        {
            _stateProvinceRepository = stateProvinceRepository;
            _orgTypeRepository = orgTypeRepository;
            _organizationRepository = organizationRepository;
            _messageProducer = messageProducer;
        }

        public async Task<Guid> HandleCommand(AddOrganizationCommand cmd)
        {
            //lookup the StateProvince
            var state = _stateProvinceRepository.Get(cmd.StateProvinceId);
            if (state == null)
                throw new FormatException("Submitted State/Province is invalid.");

            //lookup the OrgType
            var orgType = _orgTypeRepository.Get(cmd.OrgTypeId);
            if (orgType == null)
                throw new FormatException("Invalid Organization type submitted.");

            //set the address
            var address = new Address(cmd.Address1, cmd.Address2, cmd.City, state, cmd.PostalCode, cmd.AddressType);

            var org = new Organization(cmd.Name, cmd.DbaName, cmd.EIN, address, orgType, cmd.ParentId, cmd.Active, cmd.Verified);
            _organizationRepository.Add(org);
            _organizationRepository.Save();

            //fire event here
            await FireOrgAddedEvent(org, cmd);

            return org.ID;
        }


        private async Task<bool> FireOrgAddedEvent(Organization org, AddOrganizationCommand cmd)
        {
            var addressDtos = new List<AddressDto>();
            addressDtos.AddRange(org.Addresses.Select(x => new AddressDto
            {
                Address1 = x.Address1,
                Address2 = x.Address2,
                City = x.City,
                ID = x.ID,
                PostalCode = x.PostalCode,
                StateProvince = x.AddressStateProvince.Name,
                TypeOfAddress = x.TypeOfAddress
            }));
            await _messageProducer.ProduceEventAsync<OrganizationCreatedEvent>(new OrganizationCreatedEvent
            {
                CorrelationId = (cmd.CommandId == null) ? Guid.NewGuid() : (Guid)cmd.CommandId,
                EntityId = org.ID,
                Active = org.Active,
                Addresses = addressDtos,
                DbaName = org.DbaName,
                EIN = org.EIN,
                Name = org.Name,
                OrgType = new OrgTypeDto {
                    CanSell = org.OrgType.CanSell,
                    Description = org.OrgType.Description,
                    ID = org.OrgType.ID,
                    Name = org.OrgType.Name
                },
                ParentId = org.ParentId,
                TimeStamp = org.Created,
                Verified = org.Verified
            });

            return true;
        }
    }
}
