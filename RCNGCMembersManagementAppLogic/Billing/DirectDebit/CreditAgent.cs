using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class CreditAgent
    {
        BankCode bankCode;
        Dictionary<string, CreditAgentDirectDebitInitiationContract> creditAgentDirectDebitInitiationContract;

        public CreditAgent(BankCode bankCode)
        {
            this.bankCode = bankCode;
            creditAgentDirectDebitInitiationContract = new Dictionary<string,CreditAgentDirectDebitInitiationContract>();
        }

        public string LocalBankCode
        {
            get { return ""; }
        }
    }
}
