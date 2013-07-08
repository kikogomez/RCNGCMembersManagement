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

        public ClubService(string description, double cost)
        {
            this.description = description;
            this.cost = cost;
        }

        public string Description
        {
            get { return description; }
        }
        public double Cost
        {
            get { return cost; }
        }
    }
}
