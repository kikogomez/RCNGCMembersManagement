using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitRemmitance
    {
        string messageID;
        DateTime creationDateTime;
        int numberOfTransactions;
        double controlSum;
        Dictionary<string, DirectDebitTransactionsGroupPayment> directDebitTransactionGroupPayment;
        DirectDebitInitiationContract directDebitInitiationContract;

        public DirectDebitRemmitance(
            DateTime creationDateTime,
            DirectDebitInitiationContract directDebitInitiationContract)
        {
            this.creationDateTime = creationDateTime;
            this.directDebitInitiationContract=directDebitInitiationContract;
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

        private void GenerateRemmitanceID()
        {
            messageID = "MSG-" + directDebitInitiationContract.CreditorID + "-" + creationDateTime.ToString("yyyyMMddHH:mm:ss");
        }
    }
}
