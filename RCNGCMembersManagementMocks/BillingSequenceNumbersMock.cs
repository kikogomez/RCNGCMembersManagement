using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementMocks
{
    public class BillingSequenceNumbersMock : IBillingSequenceNumbersManager
    {
        static uint invoiceSequenceNumber = 0;
        static uint proFormaInvoiceSequenceNumber = 0;
        static uint directDebitReferenceSequenceNumber = 0;

        public BillingSequenceNumbersMock()
        {
        }

        public uint GetInvoiceSequenceNumber()
        {
            return invoiceSequenceNumber;
        }

        public void SetInvoiceSequenceNumber(uint invoiceSequenceNumber)
        {
            BillingSequenceNumbersMock.invoiceSequenceNumber = invoiceSequenceNumber;
        }

        public uint GetProFormaInvoiceSequenceNumber()
        {
            return proFormaInvoiceSequenceNumber;
        }

        public void SetProFormaInvoiceSequenceNumber(uint proFormaInvoiceSequenceNumber)
        {
            BillingSequenceNumbersMock.proFormaInvoiceSequenceNumber = proFormaInvoiceSequenceNumber;
        }

        public uint GetDirectDebitReferenceSequenceNumber()
        {
            return directDebitReferenceSequenceNumber;
        }
  
        public void SetDirectDebitReferenceSequenceNumber(uint directDebitReferenceSequenceNumber)
        {
            BillingSequenceNumbersMock.directDebitReferenceSequenceNumber = directDebitReferenceSequenceNumber;
        }
    }
}
