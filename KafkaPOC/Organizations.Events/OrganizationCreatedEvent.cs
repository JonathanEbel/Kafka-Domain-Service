using Core;
using Organizations.Dtos;
using System;
using System.Collections.Generic;

namespace Organizations.Events
{
    public class OrganizationCreatedEvent : EventBase
    {
        public string Name { get; set; }
        public string DbaName { get; set; }
        public string EIN { get; set; }
        public Guid? ParentId { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public OrgTypeDto OrgType { get; set; }
        public bool Verified { get; set; }   
        public bool Active { get; set; }
    }
}
