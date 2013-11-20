using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementAppLogic
{
    public class DirectDebitRemmitance
    {
        string messageID;
        DateTime creationDateTime;
        int numberOfTransactions;
        double controlSum;
        Dictionary<string, DirectDebitTransactionGroupPaymentInfo> directDebitTransactionGroupPaymentInfo;
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

        private void GenerateRemmitanceID()
        {
            messageID = "MSG-" + directDebitInitiationContract.CreditorID + "-" + creationDateTime.ToString("yyyyMMddHH:mm:ss");
        }
    }
}
