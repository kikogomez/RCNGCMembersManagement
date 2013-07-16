using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.ClubServices;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class ServiceCharge: Transaction
    {
        ClubService service;
        string concept;
        int units;
        double unitCost;
        Tax tax;
        double discount;

        public ServiceCharge(ClubService service, string concept, int units, double unitCost, Tax tax, double discount)
            :base(concept, units, unitCost, tax, discount)
        {
            this.service = service;
            this.concept = concept;
            this.units = units;
            this.unitCost = unitCost;
            this.tax = tax;
            this.discount = discount;
        }
    }
}
