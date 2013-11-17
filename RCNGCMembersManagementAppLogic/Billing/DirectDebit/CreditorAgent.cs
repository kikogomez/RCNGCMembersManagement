using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class CreditorAgent
    {
        BankCode bankCode;

        public CreditorAgent(BankCode bankCode)
        {
            this.bankCode = bankCode;
        }

        public string LocalBankCode
        {
            get { return bankCode.LocalBankCode; }
        }

        public string BankName
        {
            get { return bankCode.BankName; }
        }

        public string BankBIC
        {
            get { return bankCode.BankBIC; }
        }
    }
}
