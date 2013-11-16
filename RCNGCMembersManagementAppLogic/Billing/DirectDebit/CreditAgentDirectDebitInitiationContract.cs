using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class CreditAgentDirectDebitInitiationContract
    {
        BankAccount creditorAgentAccount;
        string creditorBusinessCode;

        public CreditAgentDirectDebitInitiationContract(BankAccount creditorAgentAccount, string creditorBusinessCode)
        {
            this.creditorAgentAccount = creditorAgentAccount;
            this.creditorBusinessCode = creditorBusinessCode;
        }
    }
}
