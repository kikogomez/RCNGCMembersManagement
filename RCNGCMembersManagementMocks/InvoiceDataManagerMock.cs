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
        static uint lastInvoiceSequenceNumber = 0;

        public InvoiceDataManagerMock()
        {
        }

        public uint NextInvoiceSequenceNumber
        {
            get { return GetNextInvoiceSequenceNumber(); }
            set { SetInvoiceSequenceNumber(value); }
        }

        public uint GetNextInvoiceSequenceNumber()
        {
            return lastInvoiceSequenceNumber + 1;
        }

        public void SetInvoiceSequenceNumber(uint invoiceNumber)
        {
            InvoiceDataManagerMock.lastInvoiceSequenceNumber = invoiceNumber;
        }
    }
}
