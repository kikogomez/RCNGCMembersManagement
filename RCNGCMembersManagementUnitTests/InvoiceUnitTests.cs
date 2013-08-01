using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests.Billing
{
    [TestClass]
    public class InvoiceUnitTests
    {
        static BillingDataManager billDataManager;
        List<Transaction> transactionsList;
        Dictionary<string, Tax> taxesDictionary;
        ClubMember clubMember;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            DataManagerMock invoiceDataManagerMock = new DataManagerMock();
            billDataManager = BillingDataManager.Instance;
            billDataManager.SetDataManagerCollaborator(invoiceDataManagerMock);
  
        }
        
        [TestInitialize]
        public void Setup()
        {
            taxesDictionary = new Dictionary<string, Tax>{
                {"No IGIC", new Tax("No IGIC",0)},
                {"IGIC Reducido 1", new Tax("IGIC Reducido 1",2.75)},
                {"IGIC Reducido 2", new Tax("IGIC Reducido 2",3)},
                {"IGIC General", new Tax("IGIC General",7)},
                {"IGIC Incrementado 1", new Tax("IGIC Incrementado 1",9.50)},
                {"IGIC Incrementado 2", new Tax("IGIC Incrementado 2",13.50)},
                {"IGIC Especial", new Tax("IGIC Especial",20)}};

            transactionsList = new List<Transaction>()
            {
                {new Transaction("Monthly Fee",1,80,taxesDictionary["No IGIC"],0)},
                {new Transaction("Renting a Kajak",1,50,taxesDictionary["No IGIC"],0)},
                {new Transaction("Blue cup",2,10,taxesDictionary["No IGIC"],0)},
                {new Transaction("BIG Mouring",1,500,taxesDictionary["No IGIC"],0)}
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
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void AnInvoiceDetailCantBeEmpty()
        {
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>();
            try
            {
                Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            }
            catch (ArgumentNullException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("The invoice detail can't be empty", exceptionMessages[0]);
                throw exception;
            }
        }

        [TestMethod]
        public void AFreshlyCreatedInvoiceIsSetToBePaid()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual(Invoice.InvoicePaymentState.ToBePaid, invoice.InvoiceState);
        }

        [TestMethod]
        public void InvoiceCustomerDataIsWellStoredAndReadable()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual("Francisco Gomez", invoice.CustomerFullName);
        }

        [TestMethod]
        public void IssueDateIsWellStoredAndReadable()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual(issueDate, invoice.IssueDate);
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
            billDataManager.InvoiceSequenceNumber=5000;
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            int invoiceSequenceNumber = int.Parse(invoice.InvoiceID.Substring(invoice.InvoiceID.Length - 6));
            Assert.AreEqual(5000, invoiceSequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberOver999999()
        {
            try
            {
                billDataManager.InvoiceSequenceNumber=1000000;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Max 999999 invoices per year", exceptionMessages[0]);
                throw exception;
            }
            billDataManager.InvoiceSequenceNumber=1000000;
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberTo0()
        {
            Assert.Inconclusive();
            //billDataManager.SetLastInvoiceNumber(0);
        }

        [TestMethod]
        public void ICanInstantiateAnInvoiceWithAGivenInvoiceID()
        {
            string invoiceID = "INV20130012345";
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceID, clubMember, transactionsList, issueDate);
            Assert.AreEqual(invoiceID, invoice.InvoiceID);
        }

        [TestMethod]
        public void InstantiatingAnInvoiceWithAGivenInvoiceIDDoesntChangeTheInvoiceIDSequenceNumber()
        {
            billDataManager.InvoiceSequenceNumber = (5000);
            //billDataManager.SetInvoiceSequenceNumber(5000);
            string invoiceID = "INV20130012345";
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceID, clubMember, transactionsList, issueDate);
            Assert.AreEqual((uint)5000, billDataManager.InvoiceSequenceNumber);
        }

        [TestMethod]
        public void TheInvoiceTransactionsCanHaveEachDifferentTaxes()
        {
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new Transaction("Nice Blue Cap", 1,10,new Tax("5% Tax",5),0),
                new Transaction("Nice Blue T-Shirt", 1,20,new Tax("10% Tax",10),0)};
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual((decimal)32.5, invoice.NetAmount);
        }

        [TestMethod]
        public void TheInvoiceCanMixSalesAndServiceCharges()
        {
            Product cap = new Product("Cap", 10, new Tax("5% Tax", 5));
            ClubService membership = new ClubService("Club Full Membership", 80, new Tax("5% Tax", 5));
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new Sale(cap, "Nice Blue Cap", 1,0),
                new ServiceCharge(membership, "June Membership Fee", 1,0)};
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual((decimal)94.5, invoice.NetAmount);
        }

        [TestMethod]
        public void AnInvoiceCanMixTransactionsWithDifferentTaxes()
        {
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new Transaction("Nice Blue Cap", 1,10,new Tax("5% Tax", 5),0),
                new Transaction("June Membership Fee", 1,40,new Tax("No Tax", 0),0)};
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual((decimal)50.5, invoice.NetAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TransactionsOnInvoicesCantHaveZeroOrLessUnits()
        {
            Product cap = new Product("Cap", 5, taxesDictionary["IGIC General"]);
            ClubService membership = new ClubService("Club Full Membership", 50, taxesDictionary["IGIC General"]);
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new ServiceCharge(membership, "June Membership Fee", 0,79,taxesDictionary["IGIC General"],0)};
            try
            {
                Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Invoice transactions must have at least one element to transact", exceptionMessages[0]);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TransactionsOnProFormaInvoicesCantHaveZeroOrLessUnits()
        {
            Product cap = new Product("Cap", 5, taxesDictionary["IGIC General"]);
            ClubService membership = new ClubService("Club Full Membership", 50, taxesDictionary["IGIC General"]);
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new ServiceCharge(membership, "June Membership Fee", 0,79,taxesDictionary["IGIC General"],0)};
            try
            {
                ProFormaInvoice proFormaInvoice = new ProFormaInvoice(clubMember, transactionsList, issueDate);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Pro Forma Invoice transactions must have at least one element to transact", exceptionMessages[0]);
                throw exception;
            }
        }


        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TransactionsCantHaveNegativeCost()
        {
            DateTime issueDate = DateTime.Now;
            try
            {
                Transaction transaction= new Transaction("June Membership Fee", 1,-79,taxesDictionary["IGIC General"],0);
            
            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Transactions units cost can't be negative", exceptionMessages[0]);
                throw exception;
            }
        }

        [TestMethod]
        public void InvoicesAcceptTransactionsWithZeroCost()
        {
            Product cap = new Product("Cap", 5, taxesDictionary["IGIC General"]);
            ClubService membership = new ClubService("Club Full Membership", 50, taxesDictionary["IGIC General"]);
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new Sale(cap, "Nice Blue Cap", 1,0,taxesDictionary["IGIC Reducido 2"],0),
                new ServiceCharge(membership, "June Membership Fee", 1,0,taxesDictionary["IGIC General"],0)};
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual((decimal)0, invoice.NetAmount);
        }

        [TestMethod]
        public void ProFormaInvoicesAcceptTransactionsWithZeroCost()
        {
            Product cap = new Product("Cap", 5, taxesDictionary["IGIC General"]);
            ClubService membership = new ClubService("Club Full Membership", 50, taxesDictionary["IGIC General"]);
            DateTime issueDate = DateTime.Now;
            List<Transaction> transactionsList = new List<Transaction>{
                new Sale(cap, "Nice Blue Cap", 1,0,taxesDictionary["IGIC Reducido 2"],0),
                new ServiceCharge(membership, "June Membership Fee", 1,0,taxesDictionary["IGIC General"],0)};
            ProFormaInvoice proFormaInvoice = new ProFormaInvoice(clubMember, transactionsList, issueDate);
            Assert.AreEqual((decimal)0, proFormaInvoice.NetAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SalesCanNotBeNegative()
        {
            Product cap = new Product("Cap", 5, taxesDictionary["IGIC General"]);
            DateTime issueDate = DateTime.Now;
            try
            {
                Transaction transaction = new Sale(cap,"Return a cap", 1, -10, taxesDictionary["IGIC General"], 0);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Transactions units cost can't be negative", exceptionMessages[0]);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void ServiceChargesCanNotBeNegative()
        {
            ClubService membership = new ClubService("Club Full Membership", 50, taxesDictionary["IGIC General"]);
            DateTime issueDate = DateTime.Now;
            try
            {
                Transaction transaction = new ServiceCharge(membership, "Return Member Fee", 1, -79, taxesDictionary["IGIC General"], 0);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Transactions units cost can't be negative", exceptionMessages[0]);
                throw exception;
            }
        }

        [TestMethod]
        public void TransactionsMustAcceptNegativeUnitCostsForTheCaseOfAmendingInvoices()
        {
            DateTime issueDate = DateTime.Now;
            Transaction transaction;
            try
            {
                transaction = new Transaction("AmmendingInvoice", -1, 79, taxesDictionary["IGIC General"], 0);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw exception;
            }
            Assert.AreEqual((decimal)-84.53, transaction.NetAmount);
        }

        [TestMethod]
        public void AnAmendingInvoiceHasTheSameGrossAmountThanTheAmendedInvoiceButNegative()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            AmendingInvoice amendingInvoice = new AmendingInvoice(invoice);
            Assert.AreEqual(-invoice.GrossAmount, amendingInvoice.GrossAmount);
        }

        [TestMethod]
        public void AnAmendingInvoiceHasTheSameNetAmountThanTheAmendedInvoiceButNegative()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, transactionsList, issueDate);
            AmendingInvoice amendingInvoice = new AmendingInvoice(invoice);
            Assert.AreEqual(-invoice.NetAmount, amendingInvoice.NetAmount);
        }


/*
        [TestMethod]
        public void AnAmendingInvoiceHasTheSameTransactionsThanOriginalInvoiceButWithNegativeUnits()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void WhenCancellingAnInvoiceTheInvoiceIsMarjkedAsCancelled()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void WhenCancellingAnInvoiceAnAmendingInvoiceIsCreatedForIt()
        {
            //Aqui hay ue hacer un assert de que se INVOCA a la creacion de una Amending Invoice ¿Como?
            Assert.Fail();
        }*/

    }
}
