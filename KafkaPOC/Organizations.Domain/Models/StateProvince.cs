using System;

namespace Organizations.Domain.Models
{
    public class StateProvince
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }

        public StateProvince()
        {

        }

        public StateProvince(string name, string country)
        {
            Name = string.IsNullOrEmpty(name) ? throw new FormatException("StateProvince cannot be null.") : name;
            Country = string.IsNullOrEmpty(country) ? throw new FormatException("Country cannot be null.") : country;
        }
    }
}
