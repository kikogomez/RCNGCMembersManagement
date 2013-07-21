using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class BillUnitTests
    {

        [TestMethod]
        public void TheDefaultPaymentMethodOfANewBillisCashPayment()
        {
            Bill bill = new Bill("MMM2013","An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            Assert.AreEqual(typeof(CashPayment), bill.PaymentMethod.GetType());
        }
    }
}
