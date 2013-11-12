using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic
{
    public interface IBillingSequenceNumbersManager
    {
        uint GetInvoiceSequenceNumber();
        void SetInvoiceSequenceNumber(uint invoiceSequenceNumber);
        uint GetProFormaInvoiceSequenceNumber();
        void SetProFormaInvoiceSequenceNumber(uint proFormaInvoiceSequenceNumber);
        uint GetDirectDebitReferenceSequenceNumber();
        void SetDirectDebitReferenceSequenceNumber(uint directDebitReferenceSequenceNumber);
    }
}
