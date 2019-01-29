using Organizations.Commands;
using Organizations.Domain.Models;
using Organizations.Domain.Repos;
using System;

namespace Organizations.Domain.CommandHandlers.Implementations
{
    public class AddOrganizationCommandHandler : IAddOrganizationCommandHandler
    {
        private readonly IStateProvinceRepository _stateProvinceRepository;
        private readonly IOrgTypeRepository _orgTypeRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public AddOrganizationCommandHandler(IStateProvinceRepository stateProvinceRepository, IOrgTypeRepository orgTypeRepository,
                                    IOrganizationRepository organizationRepository)
        {
            _stateProvinceRepository = stateProvinceRepository;
            _orgTypeRepository = orgTypeRepository;
            _organizationRepository = organizationRepository;
        }

        public Guid HandleCommand(AddOrganizationCommand cmd)
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

            return org.ID;
        }
    }
}
