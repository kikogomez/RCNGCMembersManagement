using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementAppLogic.MembersManaging
{
    public class ClubMember
    {
        string memberID;
        string name;
        string firstSurname;
        string secondSurname;
        PaymentMethod defaultPaymentMethod;
        List<DirectDebitMandate> directDebitmandatesList;
        Dictionary<string, Invoice> invoicesList;
        Dictionary<string, ProFormaInvoice> proFormaInvoicesList;

        public ClubMember(string memberID, string name, string firstSurname, string secondSurname)
        {
            this.memberID=memberID;
            this.name=name;
            this.firstSurname=firstSurname;
            this.secondSurname=secondSurname;
            this.defaultPaymentMethod = new CashPayment();
            directDebitmandatesList= new List<DirectDebitMandate>();
            invoicesList=new Dictionary<string, Invoice>();
            proFormaInvoicesList = new Dictionary<string, ProFormaInvoice>();
        }

        public string MemberID
        {
            get {return memberID;}
        }

        public string FullName
        {
            get { return GetFullname(); }
        }

        public PaymentMethod DefaultPaymentMethod
        {
            get { return defaultPaymentMethod; }
        }

        public void SetDefaultPaymentMethod(PaymentMethod paymentMethod)
        {
            this.defaultPaymentMethod = paymentMethod;
        }

        public void AddInvoice(Invoice invoice)
        {
            invoicesList.Add(invoice.InvoiceID, invoice);
        }

        public void AddDirectDebitMandate(DirectDebitMandate directDebitMandate)
        {
            directDebitmandatesList.Add(directDebitMandate);
        }
        private string GetFullname()
        {
            return (name ?? "" + " " + firstSurname ?? "" + " " + secondSurname ?? "").Trim();
        }
    }
}
