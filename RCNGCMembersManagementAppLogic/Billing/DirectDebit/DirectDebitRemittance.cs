using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitRemittance
    {
        string messageID;
        DateTime creationDateTime;
        int numberOfTransactions;
        decimal controlSum;
        List<DirectDebitTransactionsGroupPayment> directDebitTransactionGroupPaymentCollection;
        DirectDebitInitiationContract directDebitInitiationContract;

        public DirectDebitRemittance(
            DateTime creationDateTime,
            DirectDebitInitiationContract directDebitInitiationContract)
        {
            this.creationDateTime = creationDateTime;
            this.directDebitInitiationContract=directDebitInitiationContract;
            directDebitTransactionGroupPaymentCollection = new List<DirectDebitTransactionsGroupPayment>();
            GenerateRemmitanceID();
        }

        public string MessageID
        {
            get { return messageID; }
        }

        public DateTime CreationDate
        {
            get { return creationDateTime; }
        }

        public DirectDebitInitiationContract DirectDebitInitiationContract
        {
            get { return directDebitInitiationContract; }
        }

        public List<DirectDebitTransactionsGroupPayment> DirectDebitTransactionGroupPaymentCollection
        {
            get { return directDebitTransactionGroupPaymentCollection; }
        }

        public int NumberOfTransactions
        {
            get { return numberOfTransactions; }
        }

        public decimal ControlSum
        {
            get { return controlSum; }
        }

        public void AddDirectDebitTransactionsGroupPayment(DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment)
        {
            directDebitTransactionGroupPaymentCollection.Add(directDebitTransactionsGroupPayment);
            UpdateNumberOfDirectDebitTransactionsAndAmount();
        }

        public void UpdateNumberOfDirectDebitTransactionsAndAmount()
        {
            this.numberOfTransactions = directDebitTransactionGroupPaymentCollection.Count;
            this.controlSum = directDebitTransactionGroupPaymentCollection.Select(
                directDebitTransactionGroupPayment => directDebitTransactionGroupPayment.TotalAmount).Sum();
        }

        private void GenerateRemmitanceID()
        {
            messageID = directDebitInitiationContract.CreditorID + creationDateTime.ToString("yyyyMMddHH:mm:ss");
        }
    }
}
