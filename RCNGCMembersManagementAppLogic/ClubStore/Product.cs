﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementAppLogic.ClubStore
{
    public class Product
    {
        string description;
        double cost;
        Tax tax;

        public Product(string description, double cost, Tax tax)
        {
            if (cost < 0) throw new System.ArgumentException("Product cost can't be negative", "taxName");
            if ((description ?? "").Trim() == "") throw new System.ArgumentException("Product name can't be empty or null", "taxName");
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
        
/*        public Transaction CreateDefaultTransaction()
        {
            return new Sale(this, this.description, 1, this.cost, this.tax, 0);
        }*/
    }
}
