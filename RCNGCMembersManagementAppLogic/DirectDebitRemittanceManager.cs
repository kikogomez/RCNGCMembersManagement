using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementAppLogic
{
    public class DirectDebitRemittanceManager
    {
        public DirectDebitRemittanceManager()
        {
        }

        public DirectDebitRemittance CreateADirectDebitRemmitance(DateTime creationDateTime, DirectDebitInitiationContract directDebitInitiationContract)
        {
            DateTime creationDate = new DateTime(2013, 11, 30, 7, 15, 0);
            DirectDebitRemittance directDebitRemmitance = new DirectDebitRemittance(creationDate, directDebitInitiationContract);
            return directDebitRemmitance;
        }

        public void AddBillToRemmitanceAsANewDirectDebitTransaction(
            DirectDebitRemittance directDebitRemmitance,
            DirectDebitTransactionsGroupPayment directDebitGroupPayment,
            DirectDebitMandate directDebitmandate,
            Bill bill)
        {
            List<Bill> billsList = new List<Bill>() { bill };
            AddAListOfBillsToRemmitanceAsANewDirectDebitTransaction(
                directDebitRemmitance,
                directDebitGroupPayment,
                directDebitmandate,
                billsList);
        }

        public void AddAListOfBillsToRemmitanceAsANewDirectDebitTransaction(
            DirectDebitRemittance directDebitRemmitance,
            DirectDebitTransactionsGroupPayment directDebitGroupPayment,
            DirectDebitMandate directDebitmandate,
            List<Bill> billsList)
        {
            DirectDebitTransaction directDebitTransaction = CreateDirectDebitTransactionForListOfBills(directDebitmandate, billsList);
            directDebitGroupPayment.AddDirectDebitTransaction(directDebitTransaction);
            directDebitRemmitance.UpdateNumberOfDirectDebitTransactionsAndAmount();
        }

        public void AddBillToAnExistingDirectDebitTransactionInARemmitance(
            DirectDebitRemittance directDebitRemmitance,
            DirectDebitTransactionsGroupPayment directDebitGroupPayment,
            DirectDebitMandate directDebitmandate,
            DirectDebitTransaction directDebitTransaction,
            Bill bill)
        {
            directDebitTransaction.AddBill(bill);
            directDebitGroupPayment.UpdateNumberOfDirectDebitTransactionsAndAmount();
            directDebitRemmitance.UpdateNumberOfDirectDebitTransactionsAndAmount();
        }

/*        private DirectDebitTransaction CreateDirectDebitTransactionForBill(DirectDebitMandate directDebitmandate, Bill bill)
        {
            List<Bill> billsList = new List<Bill>() { bill };
            DirectDebitTransaction directDebitTransaction =
                CreateDirectDebitTransactionForListOfBillsBill(directDebitmandate, billsList);
            return directDebitTransaction;
        }*/

        private DirectDebitTransaction CreateDirectDebitTransactionForListOfBills(DirectDebitMandate directDebitmandate, List<Bill> billsList)
        {
            DirectDebitTransaction directDebitTransaction = new DirectDebitTransaction(
                billsList,
                directDebitmandate.InternalReferenceNumber,
                directDebitmandate.BankAccount);
            return directDebitTransaction;
        }


    }
}
