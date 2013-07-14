using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementUnitTests.Billing
{
    [TestClass]
    public class BillingUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberOver999999()
        {
            BillDataManager.Instance.SetInvoiceNumber(1000000);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberTo0()
        {
            BillDataManager.Instance.SetInvoiceNumber(0);
        }
    }
}
