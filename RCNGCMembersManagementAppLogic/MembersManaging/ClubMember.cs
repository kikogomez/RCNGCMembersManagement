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
        Dictionary<string, AmendingInvoice> amendingInvoices;

        public ClubMember(string memberID, string name, string firstSurname, string secondSurname)
        {
            if ((name ?? "").Trim() == "") throw new ArgumentException("Club Member name cannot be empty", "name");
            if ((firstSurname ?? "").Trim() == "") throw new ArgumentException("Club Member first surname cannot be empty", "firstSurname");
            this.memberID=memberID;
            this.name=name.Trim();
            this.firstSurname=firstSurname.Trim();
            this.secondSurname=(secondSurname ?? "").Trim();
            this.defaultPaymentMethod = new CashPayment();
            directDebitmandatesList= new List<DirectDebitMandate>();
            invoicesList=new Dictionary<string, Invoice>();
            proFormaInvoicesList = new Dictionary<string, ProFormaInvoice>();
            amendingInvoices=new Dictionary<string,AmendingInvoice>();
        }

        public string MemberID
        {
            get { return memberID; }
        }

        public string Name
        {
            get { return name; }
        }

        public string FirstSurname
        {
            get { return firstSurname; }
        }

        public string SecondSurname
        {
            get { return secondSurname; }
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
            return (name + " " + firstSurname + " " + (secondSurname ?? "")).Trim();
        }
    }
}
