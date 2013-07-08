using System;
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
        double taxValue;
        double discount;

        public Transaction(string concept, int units, double unitCost, double taxValue, double discount)
        {
            this.concept = concept;
            this.units = units;
            this.unitCost = unitCost;
            this.taxValue = taxValue;
            this.discount = discount;
        }

        public string Concept
        {
            get { return concept; }
        }

        public int Units
        {
            get { return units; }
        }

        public double UnitCost
        {
            get { return unitCost; }
        }

        public double TaxValue
        {
            get { return taxValue; }
        }

        public decimal GrossAmount
        {
            get { return CalculateGrossAmount(); }
        }

        public decimal NetAmount
        {
            get { return Math.Round(GrossAmount * ((decimal)(1 + taxValue / 100)), 2, MidpointRounding.AwayFromZero); }
        }

        private decimal CalculateGrossAmount()
        {
            return Math.Round((decimal)unitCost * units, 2, MidpointRounding.AwayFromZero);
        }
    }
}
