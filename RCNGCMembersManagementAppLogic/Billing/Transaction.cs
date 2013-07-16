﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Transaction
    {
        string concept;
        int units;
        double unitCost;
        Tax tax;
        double discount;

        public Transaction(string concept, int units, double unitCost, Tax tax, double discount)
        {
            this.concept = concept;
            this.units = units;
            this.unitCost = unitCost;
            this.tax = tax;
            this.discount = discount;
        }

        public decimal GrossAmount
        {
            get { return CalculateGrossAmount(); }
        }

        public decimal NetAmount
        {
            get { return CalculateNetAmount(); }
        }

        private decimal unitCostWithDiscount()
        {
            return Math.Round((decimal)unitCost * ((decimal)(1 - discount / 100)), 2, MidpointRounding.AwayFromZero);
        }

        private decimal unitCostWithTax()
        {
            return Math.Round(unitCostWithDiscount() * ((decimal)(1 + tax.TaxValue / 100)), 2, MidpointRounding.AwayFromZero);
        }

        private decimal CalculateGrossAmount()
        {
            return Math.Round(unitCostWithDiscount() * units, 2, MidpointRounding.AwayFromZero);
        }

        private decimal CalculateNetAmount()
        {
            return Math.Round(unitCostWithTax() * units, 2, MidpointRounding.AwayFromZero);
        }
    }
}
