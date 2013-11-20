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
    public partial class GeneratingDirectDebitRemmitancesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GeneratingDirectDebitRemittances.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Generating Direct Debit Remmitances", "In order to charge the bills to my club members\r\nAs an administrative assistant\r\n" +
                    "I want to generate direct debit remitances to bank", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Generating Direct Debit Remmitances")))
            {
                RCNGCMembersManagementSpecFlowBDD.GeneratingDirectDebitRemmitancesFeature.FeatureSetup(null);
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
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "NIF",
                        "Name",
                        "BIC",
                        "CreditorAgentName",
                        "LocalBankCode",
                        "CreditorBussinesCode",
                        "CreditorAccount"});
            table1.AddRow(new string[] {
                        "G35008770",
                        "Real Club Náutico de Gran Canaria",
                        "CAIXESBBXXX",
                        "CAIXABANK",
                        "2100",
                        "777",
                        "ES5621001111301111111111"});
#line 8
 testRunner.Given("My Direct Debit Initiation Contract is", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "MemberID",
                        "Name",
                        "FirstSurname",
                        "SecondSurname",
                        "Reference",
                        "Account",
                        "BIC"});
            table2.AddRow(new string[] {
                        "00001",
                        "Francisco",
                        "Gomez-Caldito",
                        "Viseas",
                        "1234",
                        "01821111601111111111",
                        "BBVAESMMXXX"});
            table2.AddRow(new string[] {
                        "00002",
                        "Pedro",
                        "Perez",
                        "Gomez",
                        "1235",
                        "21001111301111111111",
                        "CAIXESBBXXX"});
#line 12
 testRunner.Given("These Club Members", ((string)(null)), table2, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "MemberID",
                        "TransactionConcept",
                        "Amount"});
            table3.AddRow(new string[] {
                        "00001",
                        "Cuota Mensual Octubre 2013",
                        "79"});
            table3.AddRow(new string[] {
                        "00002",
                        "Cuota Mensual Octubre 2013",
                        "79"});
            table3.AddRow(new string[] {
                        "00002",
                        "Cuota Mensual Noviembre 2013",
                        "79"});
#line 17
 testRunner.Given("These bills", ((string)(null)), table3, "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Create a new direct debit remmitance")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Generating Direct Debit Remmitances")]
        public virtual void CreateANewDirectDebitRemmitance()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new direct debit remmitance", ((string[])(null)));
#line 23
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 24
 testRunner.Given("I have a I have a direct debit initiation contract", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 25
 testRunner.When("I generate a new direct debit remmitance", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
 testRunner.Then("An empty direct debit remmitance is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
