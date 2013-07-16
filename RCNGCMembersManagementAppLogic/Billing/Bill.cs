﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Bill
    {
        string billID;
        string description;
        decimal amount;
        DateTime issueDate;
        DateTime dueDate;
        billPaymentResult paymentResult;

        public Bill(string billID, string description, decimal amount, DateTime issueDate, DateTime dueDate)
        {
            this.billID = billID;
            this.description = description;
            this.amount = amount;
            this.issueDate = issueDate;
            this.dueDate = dueDate;
            this.paymentResult = (int)billPaymentResult.ToCollect;
        }

        public enum billPaymentResult { ToCollect, Paid, Unpaid };

        public decimal Amount
        {
            get { return amount; }
        }

        public billPaymentResult BillPaymentResult
        {
            get { return (billPaymentResult)paymentResult; }
        }
    }
}
