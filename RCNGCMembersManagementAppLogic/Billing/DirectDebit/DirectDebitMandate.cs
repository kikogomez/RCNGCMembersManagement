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

        DirectDebitmandateStatus status;
        string internalReferenceNumber;
        DateTime directDebitMandateCreationDate;
        BankAccount bankAccount;
        DateTime bankAccountActivationDate;
        Dictionary<DateTime, BankAccountHistoricData> bankAccountHistory;

        public DirectDebitMandate(string mandateID, DateTime directDebitMandateCreationDate, BankAccount bankAccount)
        {
            this.billingDataManager = BillingDataManager.Instance;

            this.status = DirectDebitmandateStatus.Active;
            this.directDebitMandateCreationDate = directDebitMandateCreationDate;
            this.bankAccount = bankAccount;
            this.bankAccountActivationDate = directDebitMandateCreationDate;
            this.internalReferenceNumber = mandateID;
        }

        public DirectDebitMandate(DateTime directDebitMandateCreationDate, BankAccount bankAccount)
            : this(String.Empty, directDebitMandateCreationDate, bankAccount)
        {
            GetInternalReferenceSequenceNumber();
        }

        public enum DirectDebitmandateStatus { Active, Inactive }

        public DirectDebitmandateStatus Status
        {
            get { return status; }
        }

        public string InternalReferenceNumber
        {
            get { return internalReferenceNumber; }
        }

        public DateTime DirectDebitMandateCreationDate
        {
            get { return directDebitMandateCreationDate; }
        }

        public BankAccount BankAccount
        {
            get { return bankAccount; }
        }

        public DateTime BankAccountActivationDate
        {
            get { return bankAccountActivationDate; }
        }

        private void GetInternalReferenceSequenceNumber()
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
