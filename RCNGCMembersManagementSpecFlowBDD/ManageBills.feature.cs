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
        
#line 1 "ManageBills.feature"
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
 testRunner.Given("Last generated InvoiceID is \"INV2013000023\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A single bill is automatically created for a new invoice")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ASingleBillIsAutomaticallyCreatedForANewInvoice()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A single bill is automatically created for a new invoice", ((string[])(null)));
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
 testRunner.And("The bill ID is \"INV2013000023/001\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("By default no payment method is associated to bill", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("No bills are created for a pro forma invoice")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void NoBillsAreCreatedForAProFormaInvoice()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("No bills are created for a pro forma invoice", ((string[])(null)));
#line 44
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 45
 testRunner.Given("The member uses the club service \"Rent a kajak\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 46
 testRunner.When("I generate an pro-forma invoice for the service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
 testRunner.Then("A pro-forma invoice is created for the cost of the service: 53.50", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 48
 testRunner.And("No bills are created for a pro-forma invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill can be renegotiated into instalments")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillCanBeRenegotiatedIntoInstalments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill can be renegotiated into instalments", ((string[])(null)));
#line 50
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 51
 testRunner.Given("I have an invoice with cost 650 with a single bill with ID \"INV2013000023/001\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 52
 testRunner.When("I renegotiate the bill \"INV2013000023/001\" into three instalments: 200, 200, 250 " +
                    "to pay in 30, 60 and 90 days with agreement terms \"Payment Agtreement\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 53
 testRunner.Then("The bill \"INV2013000023/001\" is marked as renegotiated", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 54
 testRunner.And("The renegotiated bill \"INV2013000023/001\" has associated the agreement terms \"Pay" +
                    "ment Agtreement\" to it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("A bill with ID \"INV2013000023/002\" and cost of 200 to be paid in 30 days is creat" +
                    "ed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("The new bill \"INV2013000023/002\" has associated the agreement terms \"Payment Agtr" +
                    "eement\" to it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("A bill with ID \"INV2013000023/003\" and cost of 200 to be paid in 60 days is creat" +
                    "ed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And("The new bill \"INV2013000023/003\" has associated the agreement terms \"Payment Agtr" +
                    "eement\" to it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.And("A bill with ID \"INV2013000023/004\" and cost of 250 to be paid in 90 days is creat" +
                    "ed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.And("The new bill \"INV2013000023/004\" has associated the agreement terms \"Payment Agtr" +
                    "eement\" to it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("I can assign an specific payment method for a single bill")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ICanAssignAnSpecificPaymentMethodForASingleBill()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I can assign an specific payment method for a single bill", ((string[])(null)));
#line 62
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill to collect is paid in cash")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillToCollectIsPaidInCash()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill to collect is paid in cash", ((string[])(null)));
#line 64
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 65
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 66
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.When("The bill is paid in cash", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
 testRunner.Then("The bill state is set to \"Paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 69
 testRunner.And("The bill payment method is set to \"Cash\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And("The bill payment date is stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And("The bill amount is deduced form the invoice total amount", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("If the invoice total to be paid is 0 the invoice is marked as \"Paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill to collect is paid by bank transfer")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillToCollectIsPaidByBankTransfer()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill to collect is paid by bank transfer", ((string[])(null)));
#line 74
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 75
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 76
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.When("The bill is paid by bank transfer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 78
 testRunner.Then("The bill state is set to \"Paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 79
 testRunner.And("The bill payment method is set to \"Bank Transfer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
 testRunner.And("The transferor account is stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("The transferee account is stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("The bill payment date is stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.And("The bill amount is deduced form the invoice total amount", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.And("If the invoice total to be paid is 0 the invoice is marked as \"Paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill to collect is paid by direct debit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillToCollectIsPaidByDirectDebit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill to collect is paid by direct debit", ((string[])(null)));
#line 86
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 87
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 88
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
 testRunner.When("The bill is paid by direct debit", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 90
 testRunner.Then("The bill state is set to \"Paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 91
 testRunner.And("The bill payment method is set to \"Direct Debitr\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
 testRunner.And("The direct debit initiation ID is stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.And("The bill payment date is stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
 testRunner.And("The bill amount is deduced form the invoice total amount", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
 testRunner.And("If the invoice total to be paid is 0 the invoice is marked as \"Paid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill is past due date")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillIsPastDueDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill is past due date", ((string[])(null)));
#line 97
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 98
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 99
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
 testRunner.When("The bill is past due date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 101
 testRunner.Then("The bill is marked as \"Unpaid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 102
 testRunner.And("The invoice containig the bill is marked as \"Unpaid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill with an associated agreement is past due date")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillWithAnAssociatedAgreementIsPastDueDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill with an associated agreement is past due date", ((string[])(null)));
#line 104
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 105
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 106
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.And("The bill has associated a payment agreement", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 108
 testRunner.When("The bill is past due date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 109
 testRunner.Then("The bill is marked as \"Unpaid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 110
 testRunner.And("The invoice containig the bill is marked as \"Unpaid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
 testRunner.And("The associated payment agreement is set to \"NotAcomplished\" for all bills involve" +
                    "d on the agreement", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.And("The associated payment agreement is set to \"NotAcomplished\" for the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A bill due date can be extended")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void ABillDueDateCanBeExtended()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A bill due date can be extended", ((string[])(null)));
#line 114
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 115
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 116
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 117
 testRunner.When("I renew the due date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 118
 testRunner.Then("The new due date is assigned to the bill", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A past due bill due date can be renewed")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Manage bills")]
        public virtual void APastDueBillDueDateCanBeRenewed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A past due bill due date can be renewed", ((string[])(null)));
#line 120
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 121
 testRunner.Given("I have an invoice with some bills", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 122
 testRunner.And("I have a bill to collect in the invoice", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 123
 testRunner.And("The bill is past due date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 124
 testRunner.When("I renew the due date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 125
 testRunner.Then("The new due date is assigned to the bill", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 126
 testRunner.And("The bill is marked as \"ToCollect\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 127
 testRunner.And("If there are no other bills marked as \"Unpaid\" the invoice is marked \"ToBePaid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
