using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests.Billing
{
    [TestClass]
    public class InvoiceUnitTests
    {
        static List<Transaction> transactionsList;
        static ClubMember clubMember;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            DataManagerMock invoiceDataManagerMock = new DataManagerMock();
            BillDataManager.Instance.SetDataManagerCollaborator(invoiceDataManagerMock);

            transactionsList = new List<Transaction>()
            {
                {new Transaction("Monthly Fee",1,80,new Tax("NOIGIC",0),0)},
                {new Transaction("Renting a Kajak",1,50,new Tax("NOIGIC",0),0)},
                {new Transaction("Blue cup",2,10,new Tax("NOIGIC",0),0)},
                {new Transaction("BIG Mouring",1,500,new Tax("NOIGIC",0),0)}
            };
            clubMember = new ClubMember("0002", "Francisco", "Gomez", "");
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
        public void InstantiatingInvoicesWithAGivenInvoiceID()
        {
            string invoiceID = "MMM20130012345";
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceID, clubMember, transactionsList, issueDate);
        }

        [TestMethod]
        public void InstantiatingInvoicesWithoutInvoiceID()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
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
