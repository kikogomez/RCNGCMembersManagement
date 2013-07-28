﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.17929
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace RCNGCMembersManagementSpecFlowBDD
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class GeneratingInvoicesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SpecFlowGenerateInvoice.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Generating Invoices", "In order to bill the club members\r\nAs an administrtative assistant\r\nI want to gen" +
                    "erate invoices", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Generating Invoices")))
            {
                RCNGCMembersManagementSpecFlowBDD.GeneratingInvoicesFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line 7
 testRunner.Given("Last generated InvoiceID is \"INV2013000023\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "MemberID",
                        "Name",
                        "FirstSurname",
                        "SecondSurname"});
            table1.AddRow(new string[] {
                        "00001",
                        "Francisco",
                        "Gomez-Caldito",
                        "Viseas"});
#line 9
 testRunner.Given("A Club Member", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Tax Type",
                        "Tax Value"});
            table2.AddRow(new string[] {
                        "No IGIC",
                        "0"});
            table2.AddRow(new string[] {
                        "IGIC Reducido 1",
                        "2.75"});
            table2.AddRow(new string[] {
                        "IGIC Reducido 2",
                        "3.00"});
            table2.AddRow(new string[] {
                        "IGIC General",
                        "7.00"});
            table2.AddRow(new string[] {
                        "IGIC Incrementado 1",
                        "9.50"});
            table2.AddRow(new string[] {
                        "IGIC Incrementado 2",
                        "13.50"});
            table2.AddRow(new string[] {
                        "IGIC Especial",
                        "20.00"});
#line 13
 testRunner.Given("This set of taxes", ((string)(null)), table2, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Service Name",
                        "Default Cost",
                        "Default Tax"});
            table3.AddRow(new string[] {
                        "Rent a kajak",
                        "50.00",
                        "IGIC General"});
            table3.AddRow(new string[] {
                        "Rent a katamaran",
                        "100.55",
                        "IGIC General"});
            table3.AddRow(new string[] {
                        "Rent a mouring",
                        "150.00",
                        "IGIC General"});
            table3.AddRow(new string[] {
                        "Full Membership Monthly Fee",
                        "79.00",
                        "No IGIC"});
#line 23
 testRunner.Given("These services", ((string)(null)), table3, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name",
                        "Default Cost",
                        "Default Tax"});
            table4.AddRow(new string[] {
                        "Pennant",
                        "10.00",
                        "IGIC General"});
            table4.AddRow(new string[] {
                        "Cup",
                        "15.00",
                        "IGIC General"});
            table4.AddRow(new string[] {
                        "Member ID Card",
                        "1.50",
                        "No IGIC"});
#line 30
 testRunner.Given("These products", ((string)(null)), table4, "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Generate an invoice for a service charge")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void GenerateAnInvoiceForAServiceCharge()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate an invoice for a service charge", ((string[])(null)));
#line 36
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 37
 testRunner.Given("The member uses the club service \"Rent a kajak\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.When("I generate an invoice for the service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("An invoice is created for the cost of the service: 53.50", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 40
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Generate an invoice for a sale")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void GenerateAnInvoiceForASale()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate an invoice for a sale", ((string[])(null)));
#line 42
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 43
 testRunner.Given("The member buys a \"Pennant\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 44
 testRunner.When("I generate an invoice for the sale", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.Then("An invoice is created for the cost of the sale: 10.70", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 46
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("The invoices ID must allways be consecutive")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void TheInvoicesIDMustAllwaysBeConsecutive()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The invoices ID must allways be consecutive", ((string[])(null)));
#line 48
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 49
 testRunner.Given("The member uses the club service \"Rent a kajak\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
 testRunner.When("I generate an invoice for the service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 51
 testRunner.Then("The generated Invoice ID should be \"INV2013000024\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 52
 testRunner.Then("The next invoice sequence number should be 25", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Up to 999999 invoices in a year")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void UpTo999999InvoicesInAYear()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Up to 999999 invoices in a year", ((string[])(null)));
#line 54
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 55
 testRunner.Given("Last generated InvoiceID is \"INV2013999999\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 56
 testRunner.Given("The member uses the club service \"Rent a mouring\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 57
 testRunner.When("I try to generate an invoice for the service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
 testRunner.Then("The application doesn\'t accept more than 999999 invoices in the year", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Generate an invoice for multiple transactions with one tax type")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void GenerateAnInvoiceForMultipleTransactionsWithOneTaxType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate an invoice for multiple transactions with one tax type", ((string[])(null)));
#line 60
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table5.AddRow(new string[] {
                        "1",
                        "Rent a kajak",
                        "Rent a kajak for one day",
                        "50.00",
                        "IGIC General",
                        "0"});
            table5.AddRow(new string[] {
                        "2",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "0"});
#line 61
 testRunner.Given("This set of service charge transactions", ((string)(null)), table5, "Given ");
#line 65
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 66
 testRunner.Then("An invoice is created for the cost of the service: 374.50", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 67
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Generate an invoice for multiple transactions with different tax type")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void GenerateAnInvoiceForMultipleTransactionsWithDifferentTaxType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate an invoice for multiple transactions with different tax type", ((string[])(null)));
#line 69
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table6.AddRow(new string[] {
                        "1",
                        "Full Membership Monthly Fee",
                        "Monthly Fee June",
                        "79.00",
                        "No IGIC",
                        "0"});
            table6.AddRow(new string[] {
                        "2",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "0"});
#line 70
 testRunner.Given("This set of service charge transactions", ((string)(null)), table6, "Given ");
#line 74
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 75
 testRunner.Then("An invoice is created for the cost of the service: 400.00", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 76
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Discounts on transactions must be applied before taxes")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void DiscountsOnTransactionsMustBeAppliedBeforeTaxes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Discounts on transactions must be applied before taxes", ((string[])(null)));
#line 78
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table7.AddRow(new string[] {
                        "1",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "20"});
#line 79
 testRunner.Given("This set of service charge transactions", ((string)(null)), table7, "Given ");
#line 82
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 83
 testRunner.Then("An invoice is created for the cost of the service: 128.40", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 84
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Rounding: Round to two decimals Away From Zero")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void RoundingRoundToTwoDecimalsAwayFromZero()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Rounding: Round to two decimals Away From Zero", ((string[])(null)));
#line 86
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table8.AddRow(new string[] {
                        "1",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "15"});
#line 87
 testRunner.Given("This set of service charge transactions", ((string)(null)), table8, "Given ");
#line 90
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
 testRunner.Then("An invoice is created for the cost of the service: 136.43", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 92
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Rounding: First calculate discount on unit, then round, then tax unit, then round" +
            ", then sum units")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void RoundingFirstCalculateDiscountOnUnitThenRoundThenTaxUnitThenRoundThenSumUnits()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Rounding: First calculate discount on unit, then round, then tax unit, then round" +
                    ", then sum units", ((string[])(null)));
#line 94
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table9.AddRow(new string[] {
                        "2",
                        "Rent a katamaran",
                        "Renta a katamaran for 2 days",
                        "100.55",
                        "IGIC General",
                        "15"});
#line 95
 testRunner.Given("This set of service charge transactions", ((string)(null)), table9, "Given ");
#line 98
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
 testRunner.Then("An invoice is created for the cost of the service: 182.90", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 100
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Transactions can have differnt cost and tax than default service ones")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void TransactionsCanHaveDifferntCostAndTaxThanDefaultServiceOnes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transactions can have differnt cost and tax than default service ones", ((string[])(null)));
#line 102
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table10.AddRow(new string[] {
                        "1",
                        "Rent a katamaran",
                        "Renta a katamaran for 2 days",
                        "90",
                        "No IGIC",
                        "0"});
#line 103
 testRunner.Given("This set of service charge transactions", ((string)(null)), table10, "Given ");
#line 106
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 107
 testRunner.Then("An invoice is created for the cost of the service: 90.00", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 108
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("We can mix services charges and sales in a single invoice")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Invoices")]
        public virtual void WeCanMixServicesChargesAndSalesInASingleInvoice()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("We can mix services charges and sales in a single invoice", ((string[])(null)));
#line 110
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Service Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table11.AddRow(new string[] {
                        "2",
                        "Rent a katamaran",
                        "Renta a katamaran for 2 days",
                        "50",
                        "IGIC General",
                        "0"});
            table11.AddRow(new string[] {
                        "2",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "20"});
#line 111
 testRunner.Given("This set of service charge transactions", ((string)(null)), table11, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Product Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table12.AddRow(new string[] {
                        "1",
                        "Cup",
                        "Blue Cup",
                        "10",
                        "IGIC General",
                        "0"});
            table12.AddRow(new string[] {
                        "1",
                        "Member ID Card",
                        "Lost ID Card Reprinted",
                        "1.50",
                        "No IGIC",
                        "50"});
#line 115
 testRunner.Given("This set of sale transactions", ((string)(null)), table12, "Given ");
#line 119
 testRunner.When("I generate an invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 120
 testRunner.Then("An invoice is created for the cost of the service: 375.25", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 121
 testRunner.And("The invoice state is \"To be paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
