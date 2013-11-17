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
        public void GivenAReferenceNumberAndABankAccountADirectDebitOrderIsCreatedAndReferenceNumberIsAccesible()
        {
            string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(DateTime.Now.Date, bankAccount, referenceNumber);
            Assert.AreEqual(referenceNumber, directDebitMandate.InternalReferenceNumber);
        }

        [TestMethod]
        public void GivenAReferenceNumberAndABankAccountADirectDebitOrderIsCreatedAndBankAccountIsAccesible()
        {
            string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(DateTime.Now.Date, bankAccount, referenceNumber);
            Assert.AreEqual("12345678061234567890", directDebitMandate.BankAccount.CCC.CCC);
        }

        [TestMethod]
        public void WeCanSetTheDirectDebitSequenceNumberValue()
        {
/*            billDataManager.DirectDebitSequenceNumber = 5000;
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(invoiceCustomerData, transactionsList, issueDate);
            int invoiceSequenceNumber = int.Parse(invoice.InvoiceID.Substring(invoice.InvoiceID.Length - 6));
            Assert.AreEqual(5000, invoiceSequenceNumber);*/
            Assert.Inconclusive();
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
