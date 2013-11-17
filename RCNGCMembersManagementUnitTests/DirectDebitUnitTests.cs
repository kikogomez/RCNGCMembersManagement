using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementMocks;
using ExtensionMethods;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class DirectDebitUnitTests
    {
        static BillingDataManager billDataManager;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            BillingSequenceNumbersMock billingSequenceNumbersMock = new BillingSequenceNumbersMock();
            billDataManager = BillingDataManager.Instance;
            billDataManager.SetBillingSequenceNumberCollaborator(billingSequenceNumbersMock);
        }


        [TestMethod]
        public void GivenAReferenceNumberADirectDebitMandateIsCorrectlyCreated()
        {
            string internalReferenceNumber = "02645";
            BankAccount bankAccount = new BankAccount(new ClientAccountCodeCCC("12345678061234567890"));
            DateTime directDebitMandateCreationDate = new DateTime(2013, 11, 11);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(internalReferenceNumber, directDebitMandateCreationDate, bankAccount);
            Assert.AreEqual(DirectDebitMandate.DirectDebitmandateStatus.Active, directDebitMandate.Status);
            Assert.AreEqual(internalReferenceNumber, directDebitMandate.InternalReferenceNumber);
            Assert.AreEqual(directDebitMandateCreationDate, directDebitMandate.DirectDebitMandateCreationDate);
            Assert.AreEqual(bankAccount, directDebitMandate.BankAccount);
            Assert.AreEqual(directDebitMandateCreationDate, directDebitMandate.BankAccountActivationDate);
        }

        [TestMethod]
        public void IfNoReferenceNumberIsProvidedASequenceNumberIsAssigned()
        {
            BankAccount bankAccount = new BankAccount(new ClientAccountCodeCCC("12345678061234567890"));
            DateTime directDebitMandateCreationDate = new DateTime(2013, 11, 11);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(directDebitMandateCreationDate, bankAccount);
            Assert.IsNotNull(directDebitMandate.InternalReferenceNumber);
        }

        [TestMethod]
        public void WeCanSetTheDirectDebitSequenceNumberValue()
        {
            billDataManager.DirectDebitSequenceNumber = 5000;
            BankAccount bankAccount = new BankAccount(new ClientAccountCodeCCC("12345678061234567890"));
            DateTime directDebitMandateCreationDate = new DateTime(2013, 11, 11);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(directDebitMandateCreationDate, bankAccount);
            Assert.AreEqual("05000", directDebitMandate.InternalReferenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberOver999999()
        {
/*            try
            {
                billDataManager.InvoiceSequenceNumber = 1000000;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual("Invoice ID out of range (1-999999)", exception.GetMessageWithoutParamName());
                throw exception;
            }*/
            Assert.Inconclusive();
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CantSetInvoiceSequenceNumberTo0()
        {
/*            try
            {
                billDataManager.InvoiceSequenceNumber = 0;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual("Invoice ID out of range (1-999999)", exception.GetMessageWithoutParamName());
                throw exception;
            }*/
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ProvidingTheLastDirectDebitReferenceNumberWas100TheNextAssignedMustBe101()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheReferenceNumberOfADirectDebitOrderCanBeChanged()
        {
            /*string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(DateTime.Now.Date, bankAccount, referenceNumber);
            directDebitMandate.ReferenceNumber = "111111111111";
            Assert.AreEqual("111111111111", directDebitMandate.ReferenceNumber);*/

            Assert.Inconclusive(); //No se puede cambiar. Se puede desactivar y crear otra
        }

        [TestMethod]
        public void TheBankAccountOfADirectDebitOrderCanBeChanged()
        {
            /*string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(DateTime.Now.Date, bankAccount, referenceNumber);
            BankAccountFields bankAccountFields = new BankAccountFields("0128", "0035", "69", "0987654321");
            BankAccount newBankAccount= new BankAccount(bankAccountFields);
            directDebitMandate.BankAccount = newBankAccount;
            Assert.AreEqual("01280035690987654321", directDebitMandate.BankAccount.CCC.CCC);*/
            Assert.Inconclusive(); //Se debe añadir el manejo del histórico
        }
    }
}
