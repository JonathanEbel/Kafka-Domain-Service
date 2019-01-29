using Core;
using System;

namespace Organizations.Commands
{
    public class AddOrganizationCommand : CommandBase
    {
        public string Name { get; set; }
        public string DbaName { get; set; }
        public string EIN { get; set; }
        public Guid? ParentId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }  //do we want to verify this Postal Code???
        public int StateProvinceId { get; set; }
        public string AddressType { get; set; }
        public int OrgTypeId{ get; set; }  
        public bool Active { get; set; }
        public bool Verified { get; set; }
    }
}
