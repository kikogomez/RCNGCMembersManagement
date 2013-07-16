using System;
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
    [Binding]
    public class SpecFlowGenerateInvoiceSteps
    {
        string lastInvoiceID;
        ClubMember clubMember;
        Dictionary<string, Tax> taxesDictionary;
        Dictionary<string, ClubService> servicesDictionary;
        Dictionary<string, Product> productsDictionary;
        InvoiceDataManagerMock invoiceDataManagerMock;

        [BeforeScenario]
        public void InitializeTransactionsList()
        {
            List<Transaction> transactionsList= new List<Transaction>();
            ScenarioContext.Current.Add("Transactions_List", transactionsList);
        }

        [Given(@"Last generated InvoiceID is ""(.*)""")]
        public void GivenLastGeneratedInvoiceIDIs(string lastInvoiceID)
        {
            this.lastInvoiceID = lastInvoiceID;
            invoiceDataManagerMock = new InvoiceDataManagerMock();
            BillDataManager.Instance.SetInvoiceDataManagerCollaborator(invoiceDataManagerMock);
            BillDataManager.Instance.SetInvoiceNumber(uint.Parse(lastInvoiceID.Substring(7)));
        }

        [Given(@"A Club Member")]
        public void GivenAClubMember(Table clientsTable)
        {
            clubMember = new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
        }

        [Given(@"This set of taxes")]
        public void GivenThisSetOfTaxes(Table taxes)
        {
            taxesDictionary = new Dictionary<string, Tax>();
            foreach (var row in taxes.Rows)
            {
                string key=row["Tax Type"];
                Tax tax = new Tax((string)row["Tax Type"], double.Parse(row["Tax Value"]));
                taxesDictionary.Add(key, tax);
            }
        }

        [Given(@"These services")]
        public void GivenTheseServices(Table services)
        {
            servicesDictionary = new Dictionary<string, ClubService>();
            foreach (var row in services.Rows)
            {
                string serviceName = row["Service Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                ClubService clubService = new ClubService(serviceName, defaultCost, taxesDictionary[defaultTax]);
                servicesDictionary.Add(serviceName, clubService);    
            }
        }

        [Given(@"These products")]
        public void GivenTheseProducts(Table products)
        {
            productsDictionary = new Dictionary<string, Product>();
            foreach (var row in products.Rows)
            {
                string productName = row["Product Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                Product product = new Product(productName, defaultCost, taxesDictionary[defaultTax]);
                productsDictionary.Add(productName, product);
            }
        }

        [Given(@"The member uses the club service ""(.*)""")]
        public void GivenTheMemberUsesTheClubService(string serviceName)
        {
            ClubService clubService = servicesDictionary[serviceName];
            ScenarioContext.Current.Add("A_Club_Service", clubService);
        }

        [When(@"I generate an invoice for the service")]
        public void WhenIGenerateAnInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;        
            try
            {
                Invoice invoice = new Invoice(clubMember, TransactionListForSingleElement((ClubService)ScenarioContext.Current["A_Club_Service"]), issueDate);
                ScenarioContext.Current.Add("Invoice", invoice);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ScenarioContext.Current.Add("Exception_On_Invoice_Creation", e);
            }    
        }

        [Then(@"An invoice is created for the cost of the service: (.*)")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheService(decimal cost)
        {
            Assert.AreEqual(cost, ((Invoice)ScenarioContext.Current["Invoice"]).NetAmount);
        }

        [Then(@"A single bill is generated for the total amount of the invoice: (.*)")]
        public void ThenASingleBillIsGeneratedForTheTotalAmountOfTheInvoice(decimal totalAmount)
        {
            Assert.AreEqual(totalAmount, ((Invoice)ScenarioContext.Current["Invoice"]).BillsTotalAmountToCollect);
        }

        [Given(@"The member buys a ""(.*)""")]
        public void GivenTheMemberBuysA(string productName)
        {
            Product product = productsDictionary[productName];
            ScenarioContext.Current.Add("A_Sold_Product", product);
        }

        [When(@"I generate an invoice for the sale")]
        public void WhenIGenerateAnInvoiceForTheSale()
        {
            DateTime issueDate = DateTime.Now;
            Invoice invoice = new Invoice(clubMember, TransactionListForSingleElement((Product)ScenarioContext.Current["A_Sold_Product"]), issueDate);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [Then(@"An invoice is created for the cost of the sale: (.*)")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheSale(decimal cost)
        {
            Assert.AreEqual(cost, ((Invoice)ScenarioContext.Current["Invoice"]).NetAmount);
        }


        [When(@"I generate a new invoice on the same year")]
        public void WhenIGenerateANewInvoiceOnTheSameYear()
        {
            DateTime issueDate = DateTime.Now;
            ClubService service = (ClubService)ScenarioContext.Current["A_Club_Service"];
            Transaction serviceCharge = new ServiceCharge(service, service.Description, 1, service.Cost, service.Tax, 0);
            List<Transaction> transactionsList = new List<Transaction>();
            transactionsList.Add(serviceCharge);
            Invoice secondInvoice = new Invoice(clubMember, transactionsList, issueDate);
            ScenarioContext.Current.Add("Second_Invoice", secondInvoice);
        }

        [Then(@"the new invoice has a consecutive invoice ID")]
        public void ThenTheNewInvoiceHasAConsecutiveInvoiceID()
        {
            Assert.AreEqual("MMM2013000024", ((Invoice)ScenarioContext.Current["Invoice"]).InvoiceID);
            Assert.AreEqual("MMM2013000025", ((Invoice)ScenarioContext.Current["Second_Invoice"]).InvoiceID);
        }

        [Then(@"The application doesn't accept more than (.*) invoices in the year")]
        public void ThenTheApplicationDoesnTAcceptMoreThanInvoicesInTheYear(int p0)
        {
            ArgumentOutOfRangeException exception = (ArgumentOutOfRangeException)ScenarioContext.Current["Exception_On_Invoice_Creation"];
            string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual("Max 999999 invoices per year", exceptionMessages[0]);
        }

        [Given(@"This set of service charge transactions")]
        public void GivenThisSetOfServiceChargeTransactions(Table transactions)
        {
            //List<Transaction> transactionsList= new List<Transaction>();
            foreach (var row in transactions.Rows)
            {
                int units= int.Parse(row["Units"]);
                string serviceName = row["Service Name"];
                string description= row["Description"];
                double unitCost= double.Parse(row["Unit Cost"]);
                Tax tax= taxesDictionary[row["Tax"]];
                double discount = double.Parse(row["Discount"]);
                ClubService clubService = servicesDictionary[serviceName];
                Transaction transaction = new ServiceCharge(clubService, description, units, unitCost, tax ,discount);
                ((List<Transaction>)ScenarioContext.Current["Transactions_List"]).Add(transaction);
            }
            //ScenarioContext.Current.Add("Transactions_List", transactionsList);
        }

        [When(@"I generate an invoice for this/these transaction/s")]
        public void WhenIGenerateAnInvoiceForThisTheseTransactionS()
        {
            Invoice invoice = new Invoice(clubMember, (List<Transaction>)ScenarioContext.Current["Transactions_List"], DateTime.Now);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [Given(@"This set of sale transactions")]
        public void GivenThisSetOfSaleTransactions(Table transactions)
        {
            foreach (var row in transactions.Rows)
            {
                int units = int.Parse(row["Units"]);
                string productName = row["Product Name"];
                string description = row["Description"];
                double unitCost = double.Parse(row["Unit Cost"]);
                Tax tax = taxesDictionary[row["Tax"]];
                double discount = double.Parse(row["Discount"]);
                Product product = productsDictionary[productName];
                Transaction transaction = new Sale(product, description, units, unitCost, tax, discount);
                ((List<Transaction>)ScenarioContext.Current["Transactions_List"]).Add(transaction);
            }
        }


        private List<Transaction> TransactionListForSingleElement(object element)
        {
            DateTime issueDate = DateTime.Now;
            Transaction transaction;
            if (element.GetType() == typeof(ClubService))
            {
                transaction = new ServiceCharge((ClubService)element, ((ClubService)element).Description, 1, ((ClubService)element).Cost, ((ClubService)element).Tax, 0);
            }
            else
            {
                transaction = new Sale((Product)element, ((Product)element).Description, 1, ((Product)element).Cost, ((Product)element).Tax, 0);
            }
            List<Transaction> transactionsList = new List<Transaction>();
            transactionsList.Add(transaction);
            return transactionsList;
        }
    }
}
