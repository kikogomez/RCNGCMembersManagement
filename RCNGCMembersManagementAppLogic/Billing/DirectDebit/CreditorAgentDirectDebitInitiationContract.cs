using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class CreditorAgentDirectDebitInitiationContract
    {
        BankAccount creditorAgentAccount;
        string creditorBusinessCode;

        public CreditorAgentDirectDebitInitiationContract(BankAccount creditorAgentAccount, string creditorBusinessCode)
        {
            this.creditorAgentAccount = creditorAgentAccount;
            this.creditorBusinessCode = creditorBusinessCode;
        }

        public BankAccount CreditorAgentAcount
        {
            get { return creditorAgentAccount; }
        }

        public string CreditorBussinessCode
        {
            get { return creditorBusinessCode; }
        }
    }
}
