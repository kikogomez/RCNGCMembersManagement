using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitTransactionsGroupPayment
    {
        string paymentInformationID;
        string localInstrument;
        List<DirectDebitTransaction> directDebitTransactionsCollection;

        int numberOfDirectDebitTransactions;
        decimal totalAmount;

        public DirectDebitTransactionsGroupPayment(string localInstrument)
        {
            this.localInstrument = localInstrument;
        }

        public string LocalInstrument
        {
            get { return localInstrument; }
        }

        public int NumberOfDirectDebitTransactions
        {
            get { return numberOfDirectDebitTransactions; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
        }

        public void AddDirectDebitTransaction(DirectDebitTransaction directDebitTransaction)
        {
            directDebitTransactionsCollection.Add(directDebitTransaction);
        }

        private void UpdateNumberOfDirectDebitTransactionsAndAmount()
        {
            this.numberOfDirectDebitTransactions = directDebitTransactionsCollection.Count;
            this.totalAmount = directDebitTransactionsCollection.Select(directDebitTransaction => directDebitTransaction.Amount).Sum();
        }

    }
}
