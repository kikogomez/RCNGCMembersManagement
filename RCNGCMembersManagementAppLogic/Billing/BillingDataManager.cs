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

        public uint NextInvoiceSequenceNumber
        {
            get { return GetNextInvoiceSequenceNumber(); }
            set { GetNextInvoiceSequenceNumber(); }
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

        private uint GetNextInvoiceSequenceNumber()
        {
            uint invoiceSequenceNumber=dataManager.GetNextInvoiceSequenceNumber();
            if (invoiceSequenceNumber < 1000000)
            {
                return invoiceSequenceNumber;
            }
            else
            {
                ArgumentOutOfRangeException e = new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
                throw e;
            }
        }

        public void SetLastInvoiceNumber(uint invoiceSequenceNumber)
        {
            if (IsValidInvoiceSequenceNumber(invoiceSequenceNumber))
            {
                dataManager.SetLastInvoiceSequenceNumber(invoiceSequenceNumber);
            }
            else
            {
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            }
        }

        public void SetNextInvoiceNumber(uint invoiceSequenceNumber)
        {
            if (IsValidInvoiceSequenceNumber(invoiceSequenceNumber))
            {
                dataManager.SetLastInvoiceSequenceNumber(invoiceSequenceNumber);
            }
            else
            {
                throw new ArgumentOutOfRangeException("invoiceSequenceNumber", "Max 999999 invoices per year");
            }
        }

        private bool IsValidInvoiceSequenceNumber(uint invoiceNumber)
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
