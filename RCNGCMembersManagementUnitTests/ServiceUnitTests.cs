using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class ServiceUnitTests
    {
        [TestMethod]
        public void InstantiatingASimpleProductThatHasADescriptionCostAndAplicapleTax()
        {
            Tax defaultTax = new Tax("5% Tax", 5);
            ClubService simpleService = new ClubService("Optimist Rent", 10, defaultTax);
            Assert.AreEqual("Optimist Rent", simpleService.Description);
            Assert.AreEqual(10,simpleService.Cost);
            Assert.AreEqual(defaultTax, simpleService.Tax);
        }
    }
}
