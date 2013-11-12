using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public sealed class BillingDataManager
    {
        private static readonly BillingDataManager instance = new BillingDataManager();

        static IBillingSequenceNumbersManager billingSequenceNumbersManager;
        
        string invoicePrefix;
        string proFormaInvoicePrefix;
        string amendingInvoicePrefix;

        private BillingDataManager()
        {
            LoadConfig();
        }

        public static BillingDataManager Instance
        {
            get { return instance; }
        }

        public uint InvoiceSequenceNumber
        {
            get { return GetInvoiceSequenceNumber(); }
            set { SetInvoiceSequenceNumber(value); }
        }

        public uint ProFormaInvoiceSequenceNumber
        {
            get { return GetProFormaInvoiceSequenceNumber(); }
            set { SetProFormaInvoiceSequenceNumber(value); }
        }

        public string InvoicePrefix
        {
            get { return invoicePrefix; }
        }

        public string ProFormaInvoicePrefix
        {
            get { return proFormaInvoicePrefix; }
        }

        public string AmendingInvoicePrefix
        {
            get { return amendingInvoicePrefix; }
        }

        public void SetBillingSequenceNumberCollaborator(IBillingSequenceNumbersManager billingSequenceNumbersManager)
        {
            BillingDataManager.billingSequenceNumbersManager = billingSequenceNumbersManager;
        }

        private uint GetInvoiceSequenceNumber()
        {
            uint invoiceSequenceNumber=billingSequenceNumbersManager.GetInvoiceSequenceNumber();
            if (invoiceSequenceNumber >= 1000000)
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            return invoiceSequenceNumber;
        }

        private void SetInvoiceSequenceNumber(uint invoiceSequenceNumber)
        {
            if (!InvoiceSequenceNuberIsInRange(invoiceSequenceNumber))
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Invoice ID out of range (1-999999)");
            billingSequenceNumbersManager.SetInvoiceSequenceNumber(invoiceSequenceNumber);
        }

        private uint GetProFormaInvoiceSequenceNumber()
        {
            uint proFormaInvoiceSequenceNumber = billingSequenceNumbersManager.GetProFormaInvoiceSequenceNumber();
            if (proFormaInvoiceSequenceNumber >= 1000000)
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            return proFormaInvoiceSequenceNumber;
        }

        private void SetProFormaInvoiceSequenceNumber(uint proFormaInvoiceSequenceNumber)
        {
            if (!InvoiceSequenceNuberIsInRange(proFormaInvoiceSequenceNumber))
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Pro Forma Invoice ID out of range (1-999999)");
            billingSequenceNumbersManager.SetProFormaInvoiceSequenceNumber(proFormaInvoiceSequenceNumber);
        }

        private bool InvoiceSequenceNuberIsInRange(uint invoiceNumber)
        {
            return (1 <= invoiceNumber && invoiceNumber < 1000000);
        }

        private void LoadConfig()
        {
            invoicePrefix = "INV";
            proFormaInvoicePrefix = "PRF";
            amendingInvoicePrefix = "AMN";
        }
    }
}
