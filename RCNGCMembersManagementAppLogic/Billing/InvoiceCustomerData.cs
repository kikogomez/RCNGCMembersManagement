using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.MembersManaging;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class InvoiceCustomerData
    {
        string fullName;

        public InvoiceCustomerData(ClubMember clubMember)
        {
            this.fullName = clubMember.FullName;
        }

        public InvoiceCustomerData()
        {
            this.fullName = "";
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
    }
}
