using System;
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
        BillPaymentResult paymentResult;
        PaymentMethod paymentMethod;

        public Bill(string billID, string description, decimal amount, DateTime issueDate, DateTime dueDate)
        {
            this.billID = billID;
            this.description = description;
            this.amount = amount;
            this.issueDate = issueDate;
            this.dueDate = dueDate;
            this.paymentResult = (int)BillPaymentResult.ToCollect;
            this.paymentMethod = new CashPayment();
        }

        public Bill(string description, decimal amount, DateTime issueDate, DateTime dueDate)
        {
            this.description = description;
            this.amount = amount;
            this.issueDate = issueDate;
            this.dueDate = dueDate;
            this.paymentResult = (int)BillPaymentResult.ToCollect;
            this.paymentMethod = new CashPayment();
        }

        public Bill(string description, decimal amount, DateTime issueDate, DateTime dueDate, PaymentMethod paymentMethod)
        {
            this.description = description;
            this.amount = amount;
            this.issueDate = issueDate;
            this.dueDate = dueDate;
            this.paymentResult = (int)BillPaymentResult.ToCollect;
            this.paymentMethod = paymentMethod;
        }

        public Bill(string billID, string description, decimal amount, DateTime issueDate, DateTime dueDate, PaymentMethod paymentMethod)
        {
            this.billID = billID;
            this.description = description;
            this.amount = amount;
            this.issueDate = issueDate;
            this.dueDate = dueDate;
            this.paymentResult = (int)BillPaymentResult.ToCollect;
            this.paymentMethod = paymentMethod;
        }

        public enum BillPaymentResult { ToCollect, Paid, Unpaid, CancelledOut, Failed };
        //public enum BillPaymentMethod { Cash, CreditCard, Check, BankTransfer, DirectDebit };

        public string BillID
        {
            get { return billID; }
            set { billID = value; }
        }
        public decimal Amount
        {
            get { return amount; }
        }

        public BillPaymentResult PaymentResult
        {
            get { return (BillPaymentResult)paymentResult; }
        }

        public PaymentMethod PaymentMethod
        {
            get { return paymentMethod; }
        }
    }
}
