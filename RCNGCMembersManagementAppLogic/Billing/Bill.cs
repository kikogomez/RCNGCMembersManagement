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
        PaymentAgreement paymentAgreement;

        public Bill(string billID, string description, decimal amount, DateTime issueDate, DateTime dueDate)
            : this(billID, description, amount, issueDate, dueDate, null) { }

        public Bill(string description, decimal amount, DateTime issueDate, DateTime dueDate)
            : this(null, description, amount, issueDate, dueDate, null) { }

        public Bill(string description, decimal amount, DateTime issueDate, DateTime dueDate, PaymentMethod paymentMethod)
            : this(null, description, amount, issueDate, dueDate, paymentMethod) { }

        public Bill(string billID, string description, decimal amount, DateTime issueDate, DateTime dueDate, PaymentMethod paymentMethod)
        {
            this.billID = billID;
            this.description = description;
            this.amount = amount;
            this.issueDate = issueDate;
            this.dueDate = dueDate.Date;
            this.paymentResult = (int)BillPaymentResult.ToCollect;
            this.paymentMethod = paymentMethod;
        }

        public enum BillPaymentResult { ToCollect, Paid, Unpaid, CancelledOut, Renegotiated, Failed };
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
            get { return paymentResult; }
            set { paymentResult = value; }
        }

        public PaymentMethod PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }

        public PaymentAgreement PaymentAgreement
        {
            get { return paymentAgreement; }
            set { paymentAgreement = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
        }
    }
}
