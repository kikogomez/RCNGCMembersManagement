using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Tax
    {
        string taxType;
        double taxValue;

        public Tax(string taxType, double taxValue)
        {
            this.taxType = taxType;
            this.taxValue = taxValue;
        }

        public string TaxType
        {
            get { return taxType; }
        }

        public double TaxValue
        {
            get { return taxValue; }
        }
    }
}
