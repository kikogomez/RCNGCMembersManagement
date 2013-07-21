using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitMandate
    {
        string referenceNumber;
        BankAccount bankAccount;

        public DirectDebitMandate(BankAccount bankAccount, string referenceNumber)
        {
            this.bankAccount = bankAccount;
            this.referenceNumber = referenceNumber;
        }

        public string ReferenceNumber
        {
            get { return referenceNumber; }
            set { referenceNumber = value; }
        }

        public BankAccount BankAcount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }
    }
}
