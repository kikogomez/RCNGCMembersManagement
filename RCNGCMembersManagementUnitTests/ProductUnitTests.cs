using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class ProductUnitTests
    {
        [TestMethod]
        public void InstantiatingASimpleProductThatHasADiscriptionCostAndAplicapleTax()
        {
            Tax defaultTax = new Tax("5% Tax", 5);
            Product simpleProduct = new Product("A Cap", 10, defaultTax);
            Assert.AreEqual("A Cap",simpleProduct.Description);
            Assert.AreEqual(10,simpleProduct.Cost);
            Assert.AreEqual(defaultTax, simpleProduct.Tax);
        }
    }
}
