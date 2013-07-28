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

        public ServiceCharge(ClubService service, string concept, int units, double discount)
            : this(service,concept,units,service.Cost, service.Tax, discount)
        {
        }

        public ServiceCharge(ClubService service, string concept, int units, double unitCost, Tax tax, double discount)
            :base(concept, units, unitCost, tax, discount)
        {
            if (unitCost < 0) throw new System.ArgumentOutOfRangeException("unitCost", "A Service Charge cost can't be negative");
            this.service = service;
            this.concept = concept;
            this.units = units;
            this.unitCost = unitCost;
            this.tax = tax;
            this.discount = discount;
        }
    }
}
