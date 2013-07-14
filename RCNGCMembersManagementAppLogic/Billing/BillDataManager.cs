using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public sealed class BillDataManager
    {
        private static readonly BillDataManager instance = new BillDataManager();

        static IInvoiceDataManager invoiceDataManager;

        private BillDataManager()
        {
        }

        public static BillDataManager Instance
        {
            get { return instance; }
        }

        public void SetInvoiceDataManagerCollaborator(IInvoiceDataManager invoiceDataMngr)
        {
            invoiceDataManager = invoiceDataMngr;
        }

        public uint GetNextInvoiceSequenceNumber()
        {
            uint invoiceSequenceNumber=invoiceDataManager.GetNextInvoiceSequenceNumber();
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

        public void SetInvoiceNumber(uint invoiceSequenceNumber)
        {
            if (IsValidInvoiceSequenceNumber(invoiceSequenceNumber))
            {
                invoiceDataManager.SetInvoiceSequenceNumber(invoiceSequenceNumber);
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
    }
}
