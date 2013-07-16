using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests.Billing
{
    [TestClass]
    public class BillingUnitTests
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            InvoiceDataManagerMock invoiceDataManagerMock = new InvoiceDataManagerMock();
            BillDataManager.Instance.SetInvoiceDataManagerCollaborator(invoiceDataManagerMock);
        }

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

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InvoicesCannotHaveAnEmptyListofTransactions()
        {
            BillDataManager.Instance.SetInvoiceNumber(100);
            ClubMember clubMember = new ClubMember("0001","Francisco","Gomez-Caldito","Viseas");
            List<Transaction> transactionsList = new List<Transaction>();
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
        }
    }
}
