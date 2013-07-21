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

        static IDataManager dataManager;

        private BillDataManager()
        {
        }

        public static BillDataManager Instance
        {
            get { return instance; }
        }

        public void SetDataManagerCollaborator(IDataManager dataMngr)
        {
            dataManager = dataMngr;
        }

        public uint GetNextInvoiceSequenceNumber()
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

        public void SetInvoiceNumber(uint invoiceSequenceNumber)
        {
            if (IsValidInvoiceSequenceNumber(invoiceSequenceNumber))
            {
                dataManager.SetInvoiceSequenceNumber(invoiceSequenceNumber);
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
