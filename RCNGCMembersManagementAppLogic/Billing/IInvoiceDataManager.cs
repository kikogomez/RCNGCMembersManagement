﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public interface IInvoiceDataManager
    {
        uint GetNextInvoiceSequenceNumber();
        void SetInvoiceSequenceNumber(uint invoiceSequenceNumber);
    }
}
