﻿using System;
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

        public Sale(Product product, string concept, int units, double unitCost, Tax tax, double discount)
            :base(concept, units, unitCost, tax, discount)
        {
            this.product = product;
            this.concept = concept;
            this.units = units;
            this.unitCost = unitCost;
            this.tax = tax;
            this.discount = discount;
        }
    }
}