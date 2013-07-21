using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementMocks
{
    public class DataManagerMock : IDataManager
    {
        static uint lastInvoiceSequenceNumber = 0;
        static uint lastDirectDebitReferenceSequenceNumber = 0;

        public DataManagerMock()
        {
        }

        public uint GetNextInvoiceSequenceNumber()
        {
            return lastInvoiceSequenceNumber + 1;
        }

        public void SetInvoiceSequenceNumber(uint invoiceNumber)
        {
            DataManagerMock.lastInvoiceSequenceNumber = invoiceNumber;
        }

        public uint GetNextDirectDebitReferenceSequenceNumber()
        {
            return lastDirectDebitReferenceSequenceNumber + 1;
        }
        public void SetDirectDebitReferenceSequenceNumber(uint invoiceSequenceNumber)
        {
            DataManagerMock.lastDirectDebitReferenceSequenceNumber = invoiceSequenceNumber;
        }
    }
}
