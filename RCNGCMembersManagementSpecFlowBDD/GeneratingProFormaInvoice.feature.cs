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
    public partial class GeneratingProFormaInvoicesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GeneratingProFormaInvoice.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Generating pro forma invoices", "In order to give estimates to the club members\r\nAs an administrtative assistant\r\n" +
                    "I want to generate pro forma invoices", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Generating pro forma invoices")))
            {
                RCNGCMembersManagementSpecFlowBDD.GeneratingProFormaInvoicesFeature.FeatureSetup(null);
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
 testRunner.Given("Last generated pro forma invoice ID is \"PRF2013000023\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Generating a pro forma invoice for a set of service charges and sales")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating pro forma invoices")]
        public virtual void GeneratingAProFormaInvoiceForASetOfServiceChargesAndSales()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a pro forma invoice for a set of service charges and sales", ((string[])(null)));
#line 37
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
                        "2",
                        "Rent a katamaran",
                        "Renta a katamaran for 2 days",
                        "50",
                        "IGIC General",
                        "0"});
            table5.AddRow(new string[] {
                        "2",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "20"});
#line 38
 testRunner.Given("This set of service charge transactions", ((string)(null)), table5, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Units",
                        "Product Name",
                        "Description",
                        "Unit Cost",
                        "Tax",
                        "Discount"});
            table6.AddRow(new string[] {
                        "1",
                        "Cup",
                        "Blue Cup",
                        "10",
                        "IGIC General",
                        "0"});
            table6.AddRow(new string[] {
                        "1",
                        "Member ID Card",
                        "Lost ID Card Reprinted",
                        "1.50",
                        "No IGIC",
                        "50"});
#line 42
 testRunner.Given("This set of sale transactions", ((string)(null)), table6, "Given ");
#line 46
 testRunner.When("I generate a pro forma invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
 testRunner.Then("A pro forma invoice is created for the cost of the service: 375.25", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A proforma invoice has no bill associated")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating pro forma invoices")]
        public virtual void AProformaInvoiceHasNoBillAssociated()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A proforma invoice has no bill associated", ((string[])(null)));
#line 49
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
                        "2",
                        "Rent a katamaran",
                        "Renta a katamaran for 2 days",
                        "50",
                        "IGIC General",
                        "0"});
            table7.AddRow(new string[] {
                        "2",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "20"});
#line 50
 testRunner.Given("This set of service charge transactions", ((string)(null)), table7, "Given ");
#line 54
 testRunner.When("I generate a pro forma invoice for this/these transaction/s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then("No bills are created for a pro forma invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("The invoice detail of a pro forma invoice can be edited")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating pro forma invoices")]
        public virtual void TheInvoiceDetailOfAProFormaInvoiceCanBeEdited()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The invoice detail of a pro forma invoice can be edited", ((string[])(null)));
#line 57
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
                        "2",
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "0"});
#line 58
 testRunner.Given("I generate a pro forma invoice for this/these transaction/s", ((string)(null)), table8, "Given ");
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
                        "Rent a mouring",
                        "Mouring May-June",
                        "150.00",
                        "IGIC General",
                        "20"});
#line 61
 testRunner.When("I change the invoice detail to these values", ((string)(null)), table9, "When ");
#line 64
 testRunner.Then("The pro forma invoice is modified reflecting the new value: 256.80", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
