using System;
using System.Globalization;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementMocks;


namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Manage invoices")]
    class ManageInvoicesFeatureSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;
        private readonly InvoiceContextData invoiceContextData;

        public ManageInvoicesFeatureSteps(
            MembersManagementContextData membersManagementContextData,
            InvoiceContextData invoiceContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
            this.invoiceContextData = invoiceContextData;
        }

        [BeforeScenario]
        public void InitializeTransactionsList()
        {
            List<Transaction> transactionsList= new List<Transaction>();
            ScenarioContext.Current.Add("Transactions_List", transactionsList);
        }

        [Given(@"Last generated InvoiceID is ""(.*)""")]
        public void GivenLastGeneratedInvoiceIDIs(string lastInvoiceID)
        {
            invoiceContextData.lastInvoiceID = lastInvoiceID;
            DataManagerMock invoiceDataManagerMock = new DataManagerMock();
            invoiceContextData.billDataManager.SetDataManagerCollaborator(invoiceDataManagerMock);
            invoiceContextData.billDataManager.InvoiceSequenceNumber=uint.Parse(lastInvoiceID.Substring(7));
        }

        [Given(@"A Club Member")]
        public void GivenAClubMember(Table clientsTable)
        {
            membersManagementContextData.clubMember= new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
        }

        [Given(@"This set of taxes")]
        public void GivenThisSetOfTaxes(Table taxes)
        {
            invoiceContextData.taxesDictionary = new Dictionary<string, Tax>();
            foreach (var row in taxes.Rows)
            {
                string key=row["Tax Type"];
                Tax tax = new Tax((string)row["Tax Type"], double.Parse(row["Tax Value"]));
                invoiceContextData.taxesDictionary.Add(key, tax);
            }
        }

        [Given(@"These services")]
        public void GivenTheseServices(Table services)
        {
            invoiceContextData.servicesDictionary = new Dictionary<string, ClubService>();
            foreach (var row in services.Rows)
            {
                string serviceName = row["Service Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                ClubService clubService = new ClubService(serviceName, defaultCost, invoiceContextData.taxesDictionary[defaultTax]);
                invoiceContextData.servicesDictionary.Add(serviceName, clubService);    
            }
        }

        [Given(@"These products")]
        public void GivenTheseProducts(Table products)
        {

            invoiceContextData.productsDictionary = new Dictionary<string, Product>();
            foreach (var row in products.Rows)
            {
                string productName = row["Product Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                Product product = new Product(productName, defaultCost, invoiceContextData.taxesDictionary[defaultTax]);
                invoiceContextData.productsDictionary.Add(productName, product);
            }
        }

        [Given(@"I have an invoice for the service ""(.*)""")]
        public void GivenIHaveAnInvoiceForTheService(string serviceName)
        {
            ClubService clubService = invoiceContextData.servicesDictionary[serviceName];
            DateTime issueDate = DateTime.Now;
            List<Transaction> serviceChargeList = new List<Transaction> { new ServiceCharge(clubService) };
            Invoice invoice = new Invoice(new InvoiceCustomerData(membersManagementContextData.clubMember), serviceChargeList, issueDate);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [When(@"I cancel the invoice")]
        public void WhenICancelTheInvoice()
        {
            ((Invoice)ScenarioContext.Current["Invoice"]).Cancel();
        }

        [Then(@"The invoice state is ""(.*)""")]
        public void ThenTheInvoiceStateIs(string invoiceState)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            invoiceState = ti.ToTitleCase(invoiceState).Replace(" ", "");
            string invoiceStateEnumToString = ((Invoice)ScenarioContext.Current["Invoice"]).InvoiceState.ToString();
            Assert.AreEqual(invoiceState,  invoiceStateEnumToString);
        }

        [Then(@"An amending invoice is created for the negative value of the original invoice: (.*)")]
        public void ThenAnAmendingInvoiceIsCreatedForTheNegativeValueOfTheOriginalInvoice(Decimal amount)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The amending invoice ID is the same than the original invoice with different prefix: ""(.*)""")]
        public void ThenTheAmendingInvoiceIDIsTheSameThanTheOriginalInvoiceWithDifferentPrefix(string amendingInvoiceID)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The taxes devolution \((.*)\) is separated from the base cost devolution \((.*)\)")]
        public void ThenTheTaxesDevolutionIsSeparatedFromTheBaseCostDevolution(Decimal taxValue, double baseCost)
        {
            ScenarioContext.Current.Pending();
        }

        private void AddTransactionsToTransactionList(List<Transaction> currentTransactionsList, Table newTransactions)
        {
            foreach (var row in newTransactions.Rows)
            {
                Transaction transaction;
                int units = int.Parse(row["Units"]);
                string elementName = row[1];
                string description = row["Description"];
                double unitCost = double.Parse(row["Unit Cost"]);
                Tax tax = invoiceContextData.taxesDictionary[row["Tax"]];
                double discount = double.Parse(row["Discount"]);
                if (newTransactions.Header.Contains("Service Name"))
                {
                    ClubService clubService = invoiceContextData.servicesDictionary[elementName];
                    transaction = new ServiceCharge(clubService, description, units, unitCost, tax, discount);
                }
                else
                {
                    Product product = invoiceContextData.productsDictionary[elementName];
                    transaction = new Sale(product, description, units, unitCost, tax, discount);
                }
                currentTransactionsList.Add(transaction);
            }
        }
    }
}
