using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementUnitTests.Billing
{
    [TestClass]
    public class TaxTestUnits
    {
        [TestMethod]
        public void CreatingATax()
        {
            Tax tax = new Tax("Canarian IGIC", 7);
            Assert.AreEqual(7, tax.TaxPercentage);
        }

        [TestMethod]
        public void TaxPercentageCanBeZero()
        {
            Tax tax = new Tax("No Tax", 0);
            Assert.AreEqual(0, tax.TaxPercentage);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TaxPercentagesCanNotBeNegative()
        {
            try
            {
                Tax tax = new Tax("Negative TAX", -5);

            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Tax percentages can't be negative", exceptionMessages[0]);
                throw exception;
            }
        }
    }
}
