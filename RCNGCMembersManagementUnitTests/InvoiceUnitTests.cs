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
        static BillDataManager billDataManager;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            DataManagerMock invoiceDataManagerMock = new DataManagerMock();
            billDataManager = BillDataManager.Instance;
            billDataManager.SetDataManagerCollaborator(invoiceDataManagerMock);

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
        public void CreatingANewInvoiceForASetOfTransactions()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.IsNotNull(invoice);
        }

        [TestMethod]
        public void InvoiceCustomerDataIsWellStored()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual("Francisco Gomez", invoice.CustomerFullName);
        }

        [TestMethod]
        public void WhenInstantiatingANewInvoiceANewInvoiceIDIsAssigned()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.IsNotNull(invoice.InvoiceID);
        }

        [TestMethod]
        public void TheLastSixCharactersOfAnInvoiceAreANumber()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            string invoiceIDLastSixCharacters = invoice.InvoiceID.Substring(invoice.InvoiceID.Length - 6);
            int number;
            Assert.IsTrue(int.TryParse(invoiceIDLastSixCharacters, out number));
        }

        [TestMethod]
        public void TheLastSixCharactersOfAnInvoiceAreASequenceNumber()
        {
            DateTime issueDate = DateTime.Now;
            Invoice firstInvoice = new Invoice(clubMember, transactionsList, issueDate);
            int firstSequenceNumber = int.Parse(firstInvoice.InvoiceID.Substring(firstInvoice.InvoiceID.Length - 6));
            Invoice secondInvoice = new Invoice(clubMember, transactionsList, issueDate);
            int secondSequenceNumber = int.Parse(secondInvoice.InvoiceID.Substring(secondInvoice.InvoiceID.Length - 6));
            Assert.AreEqual(secondSequenceNumber, firstSequenceNumber + 1);
        }

        [TestMethod]
        public void WeCanSetTheInvoiceSequenceNumberValue()
        {
            billDataManager.SetLastInvoiceNumber(5000);
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            int invoiceSequenceNumber = int.Parse(invoice.InvoiceID.Substring(invoice.InvoiceID.Length - 6));
            Assert.AreEqual(5001, invoiceSequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberOver999999()
        {
            billDataManager.SetLastInvoiceNumber(1000000);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberTo0()
        {
            billDataManager.SetLastInvoiceNumber(0);
        }

        [TestMethod]
        public void ForLoadingPurposesICanInstantiateAnInvoiceWithAGivenInvoiceID()
        {
            string invoiceID = "MMM20130012345";
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceID, clubMember, transactionsList, issueDate);
            Assert.AreEqual(invoiceID, invoice.InvoiceID);
        }

        [TestMethod]
        public void InstantiatingAnInvoiceWithAGivenInvoiceIDDoesntChangeTheInvoiceIDSequenceNumber()
        {
            billDataManager.SetLastInvoiceNumber(5000);
            string invoiceID = "MMM20130012345";
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceID, clubMember, transactionsList, issueDate);
            Assert.AreEqual((uint)5001, billDataManager.NextInvoiceSequenceNumber);
        }
        
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InvoicesCannotHaveAnEmptyListofTransactions()
        {
            BillDataManager.Instance.SetLastInvoiceNumber(100);
            ClubMember clubMember = new ClubMember("0001","Francisco","Gomez-Caldito","Viseas");
            List<Transaction> transactionsList = new List<Transaction>();
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
        }

        [TestMethod]
        public void ATransactionCanHaveTaxes()
        {
            billDataManager.SetLastInvoiceNumber(5000);
            string invoiceID = "MMM20130012345";
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceID, clubMember, transactionsList, issueDate);
            Assert.AreEqual((uint)5001, billDataManager.NextInvoiceSequenceNumber);
        }


    }
}
