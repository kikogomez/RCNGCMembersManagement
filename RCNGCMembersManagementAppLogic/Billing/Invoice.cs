using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;


namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Invoice
    {
        string invoiceID;
        string memberID;
        string clientFullName;
        List<Transaction> invoiceDetail;

        public Invoice(ClubMember clubMember, ClubService clubService)
        {
            this.invoiceID = GetNextInvoiceId();
            this.memberID = clubMember.MemberID;
            this.clientFullName = clubMember.FullName;
            invoiceDetail = new List<Transaction>();
            Transaction simpleServiceTransaction = new Transaction(clubService.Description, 1, clubService.Cost,0,0);
            invoiceDetail.Add(simpleServiceTransaction);
        }

        public decimal AmountWithTaxes
        {
            get { return 20; }
        }

        private string GetNextInvoiceId()
        {
            return "00001";
        }

        private decimal CalculateInvoiceAmounts(bool netAmount)
        {
            decimal amount = 0;
            foreach (Transaction line in invoiceDetail)
            {
                amount += (netAmount ? (decimal)line.NetAmount : line.GrossAmount);
            }
            return amount;
        }

    }
}
