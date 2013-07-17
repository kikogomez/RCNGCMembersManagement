using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementAppLogic.ClubServices
{
    public class ClubService: ITransactionable
    {
        string description;
        double cost;
        Tax tax;

        public ClubService(string description, double cost, Tax tax)
        {
            this.description = description;
            this.cost = cost;
            this.tax = tax;
        }

        public string Description
        {
            get { return description; }
        }
        
        public double Cost
        {
            get { return cost; }
        }

        public Tax Tax
        {
            get { return tax; }
        }

        public Transaction CreateDefaultTransaction()
        {
            return new ServiceCharge(this, this.description, 1, this.cost, this.tax, 0);
        }
    }
}
