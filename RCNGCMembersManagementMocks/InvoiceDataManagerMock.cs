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
        static int lastInvoiceNumber = 0;

        public InvoiceDataManagerMock()
        {
        }

        public int GetNextInvoiceNumber()
        {
            if (lastInvoiceNumber < 999999)
            {
                lastInvoiceNumber++;
                return lastInvoiceNumber;
            }
            else
            {
                //Exception e = new Exception("Only 999999 invoices per year");
                Exception e = new Exception("DumbMessage");
                throw e;
            }
        }

        public void SetInvoiceNumber(int invoiceNumber)
        {
            InvoiceDataManagerMock.lastInvoiceNumber = invoiceNumber;
        }
    }
}
