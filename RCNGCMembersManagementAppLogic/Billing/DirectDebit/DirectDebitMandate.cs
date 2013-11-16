using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitMandate
    {
        DateTime directDebitMandateCreationDate;
        string referenceNumber;
        DateTime bankAccountActivationDate;
        BankAccount bankAccount;
        Dictionary<DateTime, BankAccountHistoricData> bankAccountHistory;

        public DirectDebitMandate(DateTime directDebitMandateCreationDate, BankAccount bankAccount, string mandateID)
        {
            this.directDebitMandateCreationDate = directDebitMandateCreationDate;
            this.bankAccount = bankAccount;
            this.referenceNumber = mandateID;
        }

        public DirectDebitMandate(DateTime directDebitMandateCreationDate, BankAccount bankAccount)
            :this(directDebitMandateCreationDate, bankAccount,String.Empty)
        {
            //GenerateMandateID();
        }

        public string ReferenceNumber
        {
            get { return referenceNumber; }
            set { referenceNumber = value; }
        }

        public BankAccount BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }

        class BankAccountHistoricData
        {
            DateTime accountActivationDate;
            DateTime accountDeactivationDate;
            BankAccount bankAccount;
        }
    }
}
