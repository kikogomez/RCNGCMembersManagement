using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.ClubServices
{
    public class ClubService
    {
        string description;
        double cost;
        double tax;

        public ClubService(string description, double cost, double tax)
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

        public double Tax
        {
            get { return tax; }
        }
    }
}
