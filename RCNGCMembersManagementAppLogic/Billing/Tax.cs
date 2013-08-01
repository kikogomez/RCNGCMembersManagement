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
            if (taxValue < 0) throw new System.ArgumentOutOfRangeException("taxValue", "Tax percentages can't be negative");
            this.taxType = taxType;
            this.taxValue = taxValue;
        }

        public double TaxPercentage
        {
            get { return taxValue; }
        }
    }
}
