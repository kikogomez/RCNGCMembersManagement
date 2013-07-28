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
    public class MembersManagementContextData
    {
        public ClubMember clubMember;
    }

    public class InvoiceContextData
    {
        public Dictionary<string, Tax> taxesDictionary;
        public Dictionary<string, ClubService> servicesDictionary;
        public Dictionary<string, Product> productsDictionary;
        public List<Transaction> tansactionsList;
        public string lastInvoiceID;
        public BillingDataManager billDataManager = BillingDataManager.Instance;
    }

    [Binding]
    class SpecFlowInvoicesSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;
        private readonly InvoiceContextData invoiceContextData;

        public SpecFlowInvoicesSteps(
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
            invoiceContextData.billDataManager.SetLastInvoiceNumber(uint.Parse(lastInvoiceID.Substring(7)));
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

        [Given(@"The member uses the club service ""(.*)""")]
        public void GivenTheMemberUsesTheClubService(string serviceName)
        {
            ClubService clubService = invoiceContextData.servicesDictionary[serviceName];
            ScenarioContext.Current.Add("A_Club_Service", clubService);
        }

        [Given(@"The member buys a ""(.*)""")]
        public void GivenTheMemberBuysA(string productName)
        {
            Product product = invoiceContextData.productsDictionary[productName];
            ScenarioContext.Current.Add("A_Sold_Product", product);
        }

        [Given(@"This set of service charge transactions")]
        public void GivenThisSetOfServiceChargeTransactions(Table transactions)
        {
            AddTransactionsToTransactionList((List<Transaction>)ScenarioContext.Current["Transactions_List"], transactions);
        }

        [Given(@"This set of sale transactions")]
        public void GivenThisSetOfSaleTransactions(Table transactions)
        {
            AddTransactionsToTransactionList((List<Transaction>)ScenarioContext.Current["Transactions_List"], transactions);
        }

        [Given(@"I have an invoice for the service ""(.*)""")]
        public void GivenIHaveAnInvoiceForTheService(string serviceName)
        {
            ClubService clubService = invoiceContextData.servicesDictionary[serviceName];
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(membersManagementContextData.clubMember, TransactionListForSingleElement(clubService), issueDate);
            ScenarioContext.Current.Add("Invoice", invoice);
        }


        [When(@"I generate an invoice for the service")]
        public void WhenIGenerateAnInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(membersManagementContextData.clubMember, TransactionListForSingleElement((ClubService)ScenarioContext.Current["A_Club_Service"]), issueDate);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [Given(@"I generate a pro forma invoice for this/these transaction/s")]
        public void GivenIGenerateAProFormaInvoiceForThisTheseTransactionS(Table transactions)
        {
            List<Transaction> transactionsList = new List<Transaction>();
            AddTransactionsToTransactionList(transactionsList, transactions);
            ProFormaInvoice proFormaInvoice = new ProFormaInvoice(membersManagementContextData.clubMember, transactionsList, DateTime.Now);
            ScenarioContext.Current.Add("ProFormaInvoice", proFormaInvoice);
        }

        [When(@"I generate an invoice for the sale")]
        public void WhenIGenerateAnInvoiceForTheSale()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(membersManagementContextData.clubMember, TransactionListForSingleElement((Product)ScenarioContext.Current["A_Sold_Product"]), issueDate);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [When(@"I generate an invoice for this/these transaction/s")]
        public void WhenIGenerateAnInvoiceForThisTheseTransactionS()
        {
            Invoice invoice = new Invoice(membersManagementContextData.clubMember, (List<Transaction>)ScenarioContext.Current["Transactions_List"], DateTime.Now);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [When(@"I generate a pro forma invoice for this/these transaction/s")]
        public void WhenIGenerateAProFormaInvoiceForThisTheseTransactionS()
        {
            ProFormaInvoice proFormaInvoice = new ProFormaInvoice(membersManagementContextData.clubMember, (List<Transaction>)ScenarioContext.Current["Transactions_List"], DateTime.Now);
            ScenarioContext.Current.Add("ProFormaInvoice", proFormaInvoice);
        }

        [When(@"I change the invoice detail to these values")]
        public void WhenIChangeTheInvoiceDetailToTheseValues(Table transactions)
        {
            List<Transaction> transactionsList = new List<Transaction>();
            AddTransactionsToTransactionList(transactionsList, transactions);
            ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).SetNewInvoiceDetail(transactionsList);
        }

        [When(@"I try to generate an invoice for the service")]
        public void WhenITryToGenerateAnInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;
            try
            {
                Invoice invoice = new Invoice(membersManagementContextData.clubMember, TransactionListForSingleElement((ClubService)ScenarioContext.Current["A_Club_Service"]), issueDate);
                ScenarioContext.Current.Add("Invoice", invoice);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ScenarioContext.Current.Add("Exception_On_Invoice_Creation", e);
            }   
        }

        [When(@"I cancel the invoice")]
        public void WhenICancelTheInvoice()
        {
            ((Invoice)ScenarioContext.Current["Invoice"]).Cancel();
        }

        [Then(@"An invoice is created for the cost of the service: (.*)")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheService(decimal cost)
        {
            Assert.AreEqual(cost, ((Invoice)ScenarioContext.Current["Invoice"]).NetAmount);
        }

        [Then(@"A pro forma invoice is created for the cost of the service: (.*)")]
        public void ThenAProFormaInvoiceIsCreatedForTheCostOfTheService(Decimal cost)
        {
            Assert.AreEqual(cost, ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).NetAmount);
        }

        [Then(@"An invoice is created for the cost of the sale: (.*)")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheSale(decimal cost)
        {
            Assert.AreEqual(cost, ((Invoice)ScenarioContext.Current["Invoice"]).NetAmount);
        }

        [Then(@"The invoice state is ""(.*)""")]
        public void ThenTheInvoiceStateIs(string invoiceState)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            invoiceState = ti.ToTitleCase(invoiceState).Replace(" ", "");
            string invoiceStateEnumToString = ((Invoice)ScenarioContext.Current["Invoice"]).InvoiceState.ToString();
            Assert.AreEqual(invoiceState,  invoiceStateEnumToString);
        }

        [Then(@"The generated Invoice ID should be ""(.*)""")]
        public void ThenTheGeneratedInvoiceIDShouldBe(string invoiceID)
        {
            Assert.AreEqual(invoiceID, ((Invoice)ScenarioContext.Current["Invoice"]).InvoiceID);
        }

        [Then(@"The next invoice sequence number should be (.*)")]
        public void ThenTheNextInvoiceSequenceNumberShouldBe(int invoiceSequenceNumber)
        {
            Assert.AreEqual((uint)invoiceSequenceNumber, invoiceContextData.billDataManager.NextInvoiceSequenceNumber);
        }

        [Then(@"The application doesn't accept more than (.*) invoices in the year")]
        public void ThenTheApplicationDoesnTAcceptMoreThanInvoicesInTheYear(int p0)
        {
            ArgumentOutOfRangeException exception = (ArgumentOutOfRangeException)ScenarioContext.Current["Exception_On_Invoice_Creation"];
            string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual("Max 999999 invoices per year", exceptionMessages[0]);
        }

        [Then(@"The pro forma invoice is modified reflecting the new value: (.*)")]
        public void ThenTheProFormaInvoiceIsModifiedReflectingTheNewValue(Decimal amount)
        {
            Assert.AreEqual(amount, ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).NetAmount);
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



        private List<Transaction> TransactionListForSingleElement(ITransactionable element)
        {
            DateTime issueDate = DateTime.Now;
            Transaction transaction = element.CreateDefaultTransaction();
            List<Transaction> transactionsList = new List<Transaction>();
            transactionsList.Add(transaction);
            return transactionsList;
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
