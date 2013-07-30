using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;

namespace RCNGCMembersManagementUnitTests.Billing
{

    [TestClass]
    public class TransactionUnitTests
    {
        Dictionary<string, Tax> taxesDictionary;

        [TestInitialize]
        public void Setup()
        {
            taxesDictionary = new Dictionary<string, Tax>{
                {"No IGIC", new Tax("No IGIC",0)},
                {"IGIC Reducido 1", new Tax("IGIC Reducido 1",2.75)},
                {"IGIC Reducido 2", new Tax("IGIC Reducido 2",3)},
                {"IGIC General", new Tax("IGIC General",7)},
                {"IGIC Incrementado 1", new Tax("IGIC Incrementado 1",9.50)},
                {"IGIC Incrementado 2", new Tax("IGIC Incrementado 2",13.50)},
                {"IGIC Especial", new Tax("IGIC Especial",20)}};
        }

        [TestMethod]
        public void TheAmoutOfATransactionWithOneUnitHavingACostOf10Is10()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 1, 10, taxesDictionary["No IGIC"], 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
        }

        [TestMethod]
        public void TheAmoutOfATransactionWithTwoUnitsHavingACostOf10Is20()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 2, 10, taxesDictionary["No IGIC"], 0);
            Assert.AreEqual((decimal)20, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsBelowFive()
        {
            Transaction transaction = new Transaction("Cheaper than 10!", 1, 9.994, taxesDictionary["No IGIC"], 0);
            Assert.AreEqual((decimal)9.99, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsFive()
        {
            Transaction transaction = new Transaction("In fact, it's 10", 1, 9.995, taxesDictionary["No IGIC"], 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("In fact, it's 10", 1, 9.996, taxesDictionary["No IGIC"], 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
        }

        [TestMethod]
        public void ATransactionCanHaveADiscount()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 2, 10, taxesDictionary["No IGIC"], 10);
            Assert.AreEqual((decimal)18, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingDiscoutsIsWellCalculatedThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("No Applicable Discount", 1, 0.01, taxesDictionary["No IGIC"], 49);
            Assert.AreEqual((decimal)0.01, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingDiscoutsIsWellCalculatedThirdDecimalIsFive()
        {
            Transaction transaction = new Transaction("No Applicable Discount", 1, 0.01, taxesDictionary["No IGIC"], 50);
            Assert.AreEqual((decimal)0.01, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingDiscoutsIsWellCalculatedThirdDecimalIsBelowFive()
        {
            Transaction transaction = new Transaction("Ooops... it's for free!", 1, 0.01, taxesDictionary["No IGIC"], 51);
            Assert.AreEqual((decimal)0, transaction.GrossAmount);
        }

        [TestMethod]
        public void DiscountsAreCalculatedPerUnitNotOnTotal()
        {
            Transaction oneArticleOfTwoCents = new Transaction("No Applicable Discount", 1, 0.02, taxesDictionary["No IGIC"], 51);
            Transaction TwoArticlesOfOneCents = new Transaction("May I have tow thousand of theese?", 2, 0.01, taxesDictionary["No IGIC"], 51);
            Assert.AreEqual((decimal)0.01, oneArticleOfTwoCents.GrossAmount);
            Assert.AreEqual((decimal)0.00, TwoArticlesOfOneCents.GrossAmount);
        }

        [TestMethod]
        public void NegativeDiscountsAreConsideredSurcharges()
        {
            Transaction surchargedTransaction = new Transaction("No Applicable Discount", 1, 10, taxesDictionary["No IGIC"], -10);
            Assert.AreEqual((decimal)11, surchargedTransaction.GrossAmount);
        }

        [TestMethod]
        public void ATransactionCanHaveTaxes()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 1,10,taxesDictionary["IGIC General"],0);
            Assert.AreEqual((decimal)10.7, transaction.NetAmount);
        }

        [TestMethod]
        public void RoundingTaxIsWellCalculatedThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("No VAT!", 1, 0.01, taxesDictionary["No IGIC"], 49);
            Assert.AreEqual((decimal)0.01, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingTaxIsWellCalculatedThirdDecimalIsFive()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void RoundingTaxIsWellCalculatedThirdDecimalIsBelowFive()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TaxIsAppliedPerUnitNotToTotal()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TaxIsAppliedAfterDiscount()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TransactionsCantHaveNegativeUnitCosts()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TransactionsCanHaveNegativeUnitNumbersToReflectDevolutionsOrCancellationsOnAmendingInvoices()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheAmoutOfATransacationCanBeNegative()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void RoundingNegativeValues()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TaxesPercentagesCannotBeNegative()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheTaxAmountOfANegativeGrossAmountIsNegative()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AppliyinNegativeUnitToPositive()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TransactionsCanBeSales()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TransactionsCanBeServiceCharges()
        {
            Assert.Inconclusive();
        }
    }
}
