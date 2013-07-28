using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.ClubStore;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Sale: Transaction
    {
        Product product;
        string concept;
        int units;
        double unitCost;
        Tax tax;
        double discount;

        public Sale(Product product, string concept, int units, double discount)
            : this(product,concept,units, product.Cost, product.Tax,discount)
        {
        }

        public Sale(Product product, string concept, int units, double unitCost, Tax tax, double discount)
            :base(concept, units, unitCost, tax, discount)
        {
            if (unitCost < 0) throw new System.ArgumentOutOfRangeException("unitCost", "A Product cost can't be negative");
            this.product = product;
            this.concept = concept;
            this.units = units;
            this.unitCost = unitCost;
            this.tax = tax;
            this.discount = discount;
        }
    }
}
