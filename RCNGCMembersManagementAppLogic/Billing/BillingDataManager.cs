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

        static IDataManager dataManager;
        
        uint nextInvoiceSequenceNumber;
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

        public void SetDataManagerCollaborator(IDataManager dataMngr)
        {
            dataManager = dataMngr;
        }

        private uint GetInvoiceSequenceNumber()
        {
            uint invoiceSequenceNumber=dataManager.GetInvoiceSequenceNumber();
            if (invoiceSequenceNumber >= 1000000)
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            return invoiceSequenceNumber;
        }

        private void SetInvoiceSequenceNumber(uint invoiceSequenceNumber)
        {
            if (!InvoiceSequenceMuberIsInRange(invoiceSequenceNumber))
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            dataManager.SetInvoiceSequenceNumber(invoiceSequenceNumber);
        }

        private uint GetProFormaInvoiceSequenceNumber()
        {
            uint proFormaInvoiceSequenceNumber = dataManager.GetProFormaInvoiceSequenceNumber();
            if (proFormaInvoiceSequenceNumber >= 1000000)
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            return proFormaInvoiceSequenceNumber;
        }

        private void SetProFormaInvoiceSequenceNumber(uint proFormaInvoiceSequenceNumber)
        {
            if (!InvoiceSequenceMuberIsInRange(proFormaInvoiceSequenceNumber))
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            dataManager.SetProFormaInvoiceSequenceNumber(proFormaInvoiceSequenceNumber);
        }

        private bool InvoiceSequenceMuberIsInRange(uint invoiceNumber)
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
