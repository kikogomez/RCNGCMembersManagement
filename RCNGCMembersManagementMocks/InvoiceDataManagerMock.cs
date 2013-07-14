using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementMocks
{
    public class InvoiceDataManagerMock : IInvoiceDataManager
    {
        static int lastInvoiceSequenceNumber = 0;

        public InvoiceDataManagerMock()
        {
        }

        public int NextInvoiceSequenceNumber
        {
            get { return GetNextInvoiceSequenceNumber(); }
            set { SetInvoiceSequenceNumber(value); }
        }

        public int GetNextInvoiceSequenceNumber()
        {
            return lastInvoiceSequenceNumber + 1;
        }

        public void SetInvoiceSequenceNumber(int invoiceNumber)
        {
            InvoiceDataManagerMock.lastInvoiceSequenceNumber = invoiceNumber;
        }
    }
}
