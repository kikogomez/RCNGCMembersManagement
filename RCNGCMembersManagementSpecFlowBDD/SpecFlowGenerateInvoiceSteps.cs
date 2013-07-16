using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementMocks;


namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class SpecFlowGenerateInvoiceSteps
    {
        string lastInvoiceID;
        ClubMember clubMember;
        Dictionary<string, Tax> taxesList;
        Dictionary<string, ClubService> servicesList;
        InvoiceDataManagerMock invoiceDataManagerMock;

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
            taxesList = new Dictionary<string, Tax>();
            foreach (var row in taxes.Rows)
            {
                string key=row["Tax Type"];
                Tax tax = new Tax((string)row["Tax Type"], double.Parse(row["Tax Value"]));
                taxesList.Add(key, tax);
            }
        }

        [Given(@"This services")]
        public void GivenThisServices(Table services)
        {
            servicesList = new Dictionary<string, ClubService>();
            foreach (var row in services.Rows)
            {
                string serviceName = row["Service Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                ClubService clubService = new ClubService(serviceName, defaultCost, taxesList[defaultTax]);
                servicesList.Add(serviceName, clubService);    
            }
        }

        [Given(@"The member uses the club service ""(.*)""")]
        public void GivenTheMemberUsesTheClubService(string serviceName)
        {
            ClubService clubService = servicesList[serviceName];
            ScenarioContext.Current.Add("A_Club_Service", clubService);
        }

/*
        [Given(@"The member use a club service")]
        public void GivenTheMemberUseAClubService(Table servicesTable)
        {
            string serviceDescription = servicesTable.Rows[0]["Description"];
            double serviceCost = double.Parse(servicesTable.Rows[0]["Default Cost per Hour"]);
            Tax tax = taxesList[servicesTable.Rows[0]["Default Tax"]];
            ClubService clubService = new ClubService(serviceDescription, serviceCost, tax);
            ScenarioContext.Current.Add("A_Club_Service", clubService);
        }

  */
        [When(@"I generate an invoice for the service")]
        public void WhenIGenerateAnInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;
            try
            {
                Invoice invoice = new Invoice(clubMember, (ClubService)ScenarioContext.Current["A_Club_Service"], issueDate);
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


        [When(@"I generate a new invoice on the same year")]
        public void WhenIGenerateANewInvoiceOnTheSameYear()
        {

            DateTime issueDate = ((Invoice)ScenarioContext.Current["Invoice"]).IssueDate.AddSeconds(1);
            ClubService clubService = (ClubService)ScenarioContext.Current["A_Club_Service"];
            Invoice secondInvoice = new Invoice(clubMember, (ClubService)ScenarioContext.Current["A_Club_Service"], issueDate);
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

        [Given(@"This set of transactions")]
        public void GivenThisSetOfTransactions(Table transactions)
        {
            List<Transaction> transactionsList= new List<Transaction>();
            foreach (var row in transactions.Rows)
            {
                int units= int.Parse(row["Units"]);
                string serviceName = row["Service Name"];
                string description= row["Description"];
                double unitCost= double.Parse(row["Unit Cost"]);
                Tax tax= taxesList[row["Tax"]];
                double discount = double.Parse(row["Discount"]);
                ClubService clubService = servicesList[serviceName];
                Transaction transaction = new Transaction(clubService, description, units, unitCost, tax ,discount);
                transactionsList.Add(transaction);
            }
            ScenarioContext.Current.Add("Transactions_List", transactionsList);
        }

        [When(@"I generate an invoice for this/these transaction/s")]
        public void WhenIGenerateAnInvoiceForThisTheseTransactionS()
        {
            Invoice invoice = new Invoice(clubMember, (List<Transaction>)ScenarioContext.Current["Transactions_List"], DateTime.Now);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

    }
}
