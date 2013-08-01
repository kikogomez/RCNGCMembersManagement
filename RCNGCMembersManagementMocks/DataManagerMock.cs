using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementMocks
{
    public class DataManagerMock : IDataManager
    {
        static uint invoiceSequenceNumber = 0;
        static uint proFormaInvoiceSequenceNumber = 0;
        static uint directDebitReferenceSequenceNumber = 0;

        public DataManagerMock()
        {
        }

        public uint GetInvoiceSequenceNumber()
        {
            return invoiceSequenceNumber;
        }

        public void SetInvoiceSequenceNumber(uint invoiceSequenceNumber)
        {
            DataManagerMock.invoiceSequenceNumber = invoiceSequenceNumber;
        }

        public uint GetProFormaInvoiceSequenceNumber()
        {
            return proFormaInvoiceSequenceNumber;
        }

        public void SetProFormaInvoiceSequenceNumber(uint proFormaInvoiceSequenceNumber)
        {
            DataManagerMock.proFormaInvoiceSequenceNumber = proFormaInvoiceSequenceNumber;
        }

        public uint GetDirectDebitReferenceSequenceNumber()
        {
            return directDebitReferenceSequenceNumber;
        }
  
        public void SetDirectDebitReferenceSequenceNumber(uint directDebitReferenceSequenceNumber)
        {
            DataManagerMock.directDebitReferenceSequenceNumber = directDebitReferenceSequenceNumber;
        }
    }
}
