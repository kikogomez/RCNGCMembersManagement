using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementAppLogic
{
    public class DirectDebitRemittancesManager
    {
        public DirectDebitRemittancesManager()
        {
        }

        public DirectDebitRemittance CreateADirectDebitRemmitance(DateTime creationDateTime, DirectDebitInitiationContract directDebitInitiationContract)
        {
            DirectDebitRemittance directDebitRemmitance = new DirectDebitRemittance(creationDateTime, directDebitInitiationContract);
            return directDebitRemmitance;
        }

        public DirectDebitTransactionsGroupPayment CreateANewGroupOfDirectDebitTransactions(string localInstrument)
        {
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment = new DirectDebitTransactionsGroupPayment(localInstrument);
            return directDebitTransactionsGroupPayment;
        }

        public DirectDebitTransaction CreateANewDirectDebitTransactionFromAGroupOfBills(DirectDebitMandate directDebitmandate, List<Bill> billsList)
        {
            DirectDebitTransaction directDebitTransaction  = new DirectDebitTransaction(
                billsList,
                directDebitmandate.InternalReferenceNumber,
                directDebitmandate.BankAccount);
            return directDebitTransaction;
        }

        public void AddBilllToExistingDirectDebitTransaction(DirectDebitTransaction directDebitTransaction, Bill bill)
        {
            directDebitTransaction.AddBill(bill);
        }

        public void AddDirectDebitTransactionToGroupPayment(
            DirectDebitTransaction directDebitTransaction,
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment)
        {
            directDebitTransactionsGroupPayment.AddDirectDebitTransaction(directDebitTransaction);
        }

        public void AddDirectDebitTransactionGroupPaymentToDirectDebitRemittance(
            DirectDebitRemittance directDebitRemmitance,
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment)
        {
            directDebitRemmitance.AddDirectDebitTransactionsGroupPayment(directDebitTransactionsGroupPayment);
        }
    }
}
