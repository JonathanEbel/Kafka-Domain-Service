using System;

namespace Organizations.Domain.Models
{
    public class OrgType
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool CanSell { get; private set; }

        public OrgType()
        {

        }

        public OrgType(string name, string description)
        {
            Name = string.IsNullOrEmpty(name) ? throw new Exception("Name must have a value.") : name;
            Description = description;
        }
    }
}
