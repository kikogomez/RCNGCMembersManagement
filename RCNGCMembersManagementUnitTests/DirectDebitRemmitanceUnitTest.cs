using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementMocks;
using RCNGCMembersManagementAppLogic;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.MembersManaging;


namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class DirectDebitRemmitanceUnitTest
    {
        static Dictionary<string, ClubMember> clubMembers;
        static Creditor creditor;
        static CreditorAgent creditorAgent;
        static DirectDebitInitiationContract directDebitInitiationContract;
        static BankCodes spanishBankCodes;
        static InvoicesManager invoicesManager;

        //BillingDataManager billingDataManager;
        //List<Bill> unassignedBillsList;


        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            BillingSequenceNumbersMock invoiceDataManagerMock = new BillingSequenceNumbersMock();
            BillingDataManager.Instance.SetBillingSequenceNumberCollaborator(invoiceDataManagerMock);
            BillingDataManager.Instance.InvoiceSequenceNumber=5000;
            invoicesManager = new InvoicesManager();

            clubMembers = new Dictionary<string, ClubMember>()
            {
                {"00001", new ClubMember("00001", "Francisco", "Gómez-Caldito", "Viseas")},
                {"00002", new ClubMember("00002", "Pedro", "Pérez", "Gómez")}
            };

            creditor = new Creditor("G35008770", "Real Club Náutico de Gran Canaria");
            creditorAgent = new CreditorAgent(new BankCode("2100", "CaixaBank","CAIXESBBXXX"));
            directDebitInitiationContract = new DirectDebitInitiationContract(
                new BankAccount(new InternationalAccountBankNumberIBAN("ES5621001111301111111111")),
                creditor.NIF,
                "777",
                creditorAgent);

            var directDebitmandateslist = new[]
            {
                new {clubMemberID = "00001", internalReference = 1234, ccc = "21002222002222222222" },
                new {clubMemberID = "00002", internalReference = 1235, ccc = "21003333802222222222" }
            };

            foreach (var ddM in directDebitmandateslist)
            {
                DirectDebitMandate directDebitMandate = new DirectDebitMandate(
                    ddM.internalReference,
                    new DateTime(2013,11,11),
                    new BankAccount(new ClientAccountCodeCCC(ddM.ccc)));
                clubMembers[ddM.clubMemberID].AddDirectDebitMandate(directDebitMandate);
            }

            var billsList = new[]
            {
                new {clubMemberID = "00001", Amount = 79, transactionDescription = "Cuota Social Octubre 2013" },
                new {clubMemberID = "00002", Amount = 79, transactionDescription="Cuota Social Octubre 2013" },
                new {clubMemberID = "00002", Amount = 79, transactionDescription="Cuota Social Noviembre 2013"}
            };

            foreach (var bill in billsList)
            {
                List<Transaction> transaction = new List<Transaction>()
                {
                    new Transaction(bill.transactionDescription,1,bill.Amount,new Tax("NoTAX",0),0)
                };
                ClubMember clubMember = clubMembers[bill.clubMemberID];
                InvoiceCustomerData invoiceCustomerData = new InvoiceCustomerData(clubMember);
                Invoice invoice = new Invoice(invoiceCustomerData, transaction, new DateTime(2013, 11, 11));
                invoicesManager.AddInvoiceToClubMember(invoice, clubMember);
            }

            spanishBankCodes = new BankCodes(@"XMLFiles\SpanishBankCodes.xml", BankCodes.BankCodesFileFormat.XML);
        }

        [TestMethod]
        public void ADirectDebittRemmitanceInstanceIsCorrectlyCreated()
        {
            DateTime creationDate = new DateTime(2013, 11, 30, 7, 15, 0);
            DirectDebitRemittance directDebitRemmitance = new DirectDebitRemittance(creationDate, directDebitInitiationContract);
            string expectedMandateId = "MSG-ES90777G35008770-2013113007:15:00";
            Assert.AreEqual(expectedMandateId, directDebitRemmitance.MessageID);
            Assert.AreEqual(creationDate, directDebitRemmitance.CreationDate);
        }

        [TestMethod]
        public void ADirectDebitTransactionIsCorrectlyCreated()
        {
            ClubMember clubMember = clubMembers["00002"];
            List<Invoice> invoices = clubMember.InvoicesList.Values.ToList();
            List<Bill> bills = invoices.Select(invoice => invoice.Bills.ElementAt(0).Value).ToList();
            DirectDebitMandate directDebitMandate = clubMembers["00002"].DirectDebitmandates.ElementAt(0).Value;
            int internalDirectDebitReferenceNumber = directDebitMandate.InternalReferenceNumber;
            BankAccount debtorAccount = directDebitMandate.BankAccount;
            DirectDebitTransaction directDebitTransaction = new DirectDebitTransaction(bills, internalDirectDebitReferenceNumber, debtorAccount);
            Assert.AreEqual(bills, directDebitTransaction.BillsInTransaction);
            Assert.AreEqual(internalDirectDebitReferenceNumber, directDebitTransaction.InternalDirectDebitReferenceNumber);
            Assert.AreEqual(debtorAccount, directDebitTransaction.DebtorAccount);
            Assert.AreEqual((decimal)158, directDebitTransaction.Amount);
            Assert.AreEqual(2, directDebitTransaction.NumberOfBills);
        }

        [TestMethod]
        public void WhenAddingAnotherBillToADirectDebitTransactionTheAmmountAndNumberOfBillsAreCorrectlyUpdated()
        {
            ClubMember clubMember = clubMembers["00002"];
            Invoice firstInvoice = clubMember.InvoicesList.Values.ElementAt(0);
            List<Bill> bills = new List<Bill>() { firstInvoice.Bills.Values.ElementAt(0) };
            DirectDebitMandate directDebitMandate = clubMembers["00002"].DirectDebitmandates.ElementAt(0).Value;
            int internalDirectDebitReferenceNumber = directDebitMandate.InternalReferenceNumber;
            BankAccount debtorAccount = directDebitMandate.BankAccount;
            DirectDebitTransaction directDebitTransaction = new DirectDebitTransaction(bills, internalDirectDebitReferenceNumber, debtorAccount);
            Assert.AreEqual((decimal)79, directDebitTransaction.Amount);
            Assert.AreEqual(1, directDebitTransaction.NumberOfBills);
            Invoice secondInvoice = clubMember.InvoicesList.Values.ElementAt(1);
            Bill bill = secondInvoice.Bills.ElementAt(0).Value;
            directDebitTransaction.AddBill(bill);
            Assert.AreEqual((decimal)158, directDebitTransaction.Amount);
            Assert.AreEqual(2, directDebitTransaction.NumberOfBills);
        }

        [TestMethod]
        public void ADirecDebitTransactionGroupPaymnetIsCorrectlyCreated()
        {
            string localInstrument = "COR1";
            DirectDebitTransactionsGroupPayment dDTxGrpPaymentInfo = new DirectDebitTransactionsGroupPayment(localInstrument);
            Assert.AreEqual("COR1", dDTxGrpPaymentInfo.LocalInstrument);
        }

        [TestMethod]
        public void ADirectDebitTransactionIsCorrectlyAddedToGroupPayment()
        {
            string localInstrument = "COR1";
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment = new DirectDebitTransactionsGroupPayment(localInstrument);

            ClubMember clubMember = clubMembers["00002"];
            Invoice firstInvoice = clubMember.InvoicesList.Values.ElementAt(0);
            List<Bill> bills = new List<Bill>() { firstInvoice.Bills.Values.ElementAt(0) };
            DirectDebitMandate directDebitMandate = clubMembers["00002"].DirectDebitmandates.ElementAt(0).Value;
            int internalDirectDebitReferenceNumber = directDebitMandate.InternalReferenceNumber;
            BankAccount debtorAccount = directDebitMandate.BankAccount;
            DirectDebitTransaction directDebitTransaction = new DirectDebitTransaction(bills, internalDirectDebitReferenceNumber, debtorAccount);
            directDebitTransactionsGroupPayment.AddDirectDebitTransaction(directDebitTransaction);
            Assert.AreEqual(1, directDebitTransactionsGroupPayment.NumberOfDirectDebitTransactions);
            Assert.AreEqual((decimal)79, directDebitTransactionsGroupPayment.TotalAmount);
        }

        [TestMethod]
        public void APaymentGroupIsCorrectlyAddedToADirectDebitRemmitance()
        {
            DateTime creationDate = new DateTime(2013, 11, 30, 7, 15, 0);
            DirectDebitRemittance directDebitRemmitance = new DirectDebitRemittance(creationDate, directDebitInitiationContract);
            string localInstrument = "COR1";
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment = new DirectDebitTransactionsGroupPayment(localInstrument);
            directDebitRemmitance.AddDirectDebitTransactionsGroupPayment(directDebitTransactionsGroupPayment);
            Assert.AreEqual(1, directDebitRemmitance.DirectDebitTransactionGroupPaymentCollection.Count);
        }

        [TestMethod]
        public void WhenGeneratingTheDirectDebitRemmitanceAllsumsAreCheckedAndAllInternalIDsAreCreated() //Actualizar los ID al generar el mensaje 
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void GeneratingTheMessageThePaymentIDAndTheDirectDebitRemmitanceIDsAreGenerated() //Actualizar los ID al generar el mensaje 
        {
            Assert.Inconclusive();
        }

    }
}
