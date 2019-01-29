using Organizations.Domain.Models;
using System;
using Xunit;

namespace Organizations.Tests
{
    public class AddressTests
    {
        [Fact]
        public void CanCreateValidAddress()
        {
            var address = new Address("123 Sesame Street", "", "Buffalo", new StateProvince("NY", "USA"), "14217", "pickup");
            Assert.Equal("Buffalo", address.City);
        }

        [Fact]
        public void AddressTypeValidationWorks()
        {
            //the below has a fake AddressType...
            Assert.Throws<FormatException>(() => new Address("123 Sesame Street", "", "Buffalo", new StateProvince("NY", "USA"), "14217", "fake"));  
        }
    }
}
