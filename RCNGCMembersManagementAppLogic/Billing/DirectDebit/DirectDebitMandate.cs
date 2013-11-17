using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitMandate
    {
        BillingDataManager billingDataManager;

        DateTime directDebitMandateCreationDate;
        string internalReferenceNumber;
        DateTime bankAccountActivationDate;
        BankAccount bankAccount;
        Dictionary<DateTime, BankAccountHistoricData> bankAccountHistory;

        public DirectDebitMandate(DateTime directDebitMandateCreationDate, BankAccount bankAccount, string mandateID)
        {
            this.billingDataManager = BillingDataManager.Instance;

            this.directDebitMandateCreationDate = directDebitMandateCreationDate;
            this.bankAccount = bankAccount;
            this.internalReferenceNumber = mandateID;
        }

        public DirectDebitMandate(DateTime directDebitMandateCreationDate, BankAccount bankAccount)
            :this(directDebitMandateCreationDate, bankAccount,String.Empty)
        {
            GetInternalReferenceNumber();
        }

        public string InternalReferenceNumber
        {
            get { return internalReferenceNumber; }
            set { internalReferenceNumber = value; }
        }

        public BankAccount BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }

        private void GetInternalReferenceNumber()
        {
            internalReferenceNumber = billingDataManager.InvoiceSequenceNumber.ToString("00000");
            billingDataManager.InvoiceSequenceNumber++;
        }

        class BankAccountHistoricData
        {
            DateTime accountActivationDate;
            DateTime accountDeactivationDate;
            BankAccount bankAccount;
        }

    }
}
