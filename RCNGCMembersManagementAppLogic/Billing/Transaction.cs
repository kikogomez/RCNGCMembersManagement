using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Transaction
    {
        string description;
        int units;
        double unitCost;
        Tax tax;
        double discount;

        public Transaction(string description, int units, double unitCost, Tax tax, double discount)
        {
            if (unitCost < 0) throw new System.ArgumentOutOfRangeException("uniCost", "Transactions units cost can't be negative");
            this.description = description;
            this.units = units;
            this.unitCost = unitCost;
            this.tax = tax;
            this.discount = discount;
        }

        public string Description
        {
            get { return description; }
        }

        public int Units
        {
            get { return units; }
        }

        public double UnitCost
        {
            get { return unitCost; }
        }

        public Tax Tax
        {
            get { return tax; }
        }

        public double Discount
        {
            get { return discount; }
        }

        public decimal GrossAmount
        {
            get { return CalculateGrossAmount(); }
        }

        public decimal NetAmount
        {
            get { return CalculateNetAmount(); }
        }

        public decimal TaxAmount
        {
            get { return GrossAmount-TaxAmount; }
        }

        private decimal unitCostWithDiscount()
        {
            return Math.Round((decimal)unitCost * ((decimal)(1 - discount / 100)), 2, MidpointRounding.AwayFromZero);
        }

        private decimal unitCostWithTax()
        {
            return Math.Round(unitCostWithDiscount() * ((decimal)(1 + tax.TaxPercentage / 100)), 2, MidpointRounding.AwayFromZero);
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
