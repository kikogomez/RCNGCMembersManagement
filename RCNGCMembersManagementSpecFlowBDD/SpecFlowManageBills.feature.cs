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
    public partial class ManageBillsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SpecFlowManageBills.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Manage bills", "In order charge my invoices\r\nAs an administrative assistant\r\nI want reate and man" +
                    "age bills for my invoices", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Manage bills")))
            {
                RCNGCMembersManagementSpecFlowBDD.ManageBillsFeature.FeatureSetup(null);
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
 testRunner.Given("Last generated InvoiceID is \"MMM2013000023\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "MemberID",
                        "Name",
                        "FirstSurname",
                        "SecondSurname",
                        "Default Payment method",
                        "Spanish IBAN Bank Account",
                        "Direct Debit Reference Number"});
            table1.AddRow(new string[] {
                        "00001",
                        "Francisco",
                        "Gomez-Caldito",
                        "Viseas",
                        "Direct Debit",
                        "IBAN ES68 1234 5678 0612 3456 7890",
                        "12345"});
#line 9
 testRunner.Given("A Club Member with a default Payment method", ((string)(null)), table1, "Given ");
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A single bill is automatically created for a new invoice with the member\'s defaul" +
            "t payment method associated")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ASingleBillIsAutomaticallyCreatedForANewInvoiceWithTheMemberSDefaultPaymentMethodAssociated()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A single bill is automatically created for a new invoice with the member\'s defaul" +
                    "t payment method associated", ((string[])(null)));
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
 testRunner.And("A single bill To Collect is generated for the total amount of the invoice: 53.50", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("By default no payment method is associated to bill", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
