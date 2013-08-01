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
            Transaction transaction = new Transaction("Nice Blue Cap", 1, 10, new Tax("No VAT",0), 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
        }

        [TestMethod]
        public void TheAmoutOfATransactionWithTwoUnitsHavingACostOf10Is20()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 2, 10, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)20, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsBelowFive()
        {
            Transaction transaction = new Transaction("Cheaper than 10!", 1, 9.994, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)9.99, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsFive()
        {
            Transaction transaction = new Transaction("In fact, it's 10", 1, 9.995, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("In fact, it's 10", 1, 9.996, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
        }

        [TestMethod]
        public void ATransactionCanHaveADiscount()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 2, 10, new Tax("No VAT", 0), 10);
            Assert.AreEqual((decimal)18, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingDiscoutsIsWellCalculatedThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("No Applicable Discount", 1, 0.01, new Tax("No VAT", 0), 49);
            Assert.AreEqual((decimal)0.01, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingDiscoutsIsWellCalculatedThirdDecimalIsFive()
        {
            Transaction transaction = new Transaction("No Applicable Discount", 1, 0.01, new Tax("No VAT", 0), 50);
            Assert.AreEqual((decimal)0.01, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingDiscoutsIsWellCalculatedThirdDecimalIsBelowFive()
        {
            Transaction transaction = new Transaction("Ooops... it's for free!", 1, 0.01, new Tax("No VAT", 0), 51);
            Assert.AreEqual((decimal)0, transaction.GrossAmount);
        }

        [TestMethod]
        public void DiscountsAreCalculatedPerUnitNotOnTotal()
        {
            Transaction oneArticleOfTwoCents = new Transaction("Great! Half-price!", 1, 0.02, new Tax("No VAT", 0), 51);
            Transaction TwoArticlesOfOneCents = new Transaction("May I have a thousand of theese?", 2, 0.01, new Tax("No VAT", 0), 51);
            Assert.AreEqual((decimal)0.01, oneArticleOfTwoCents.GrossAmount);
            Assert.AreEqual((decimal)0.00, TwoArticlesOfOneCents.GrossAmount);
        }

        [TestMethod]
        public void NegativeDiscountsAreConsideredSurcharges()
        {
            Transaction surchargedTransaction = new Transaction("For delay in payment", 1, 10, new Tax("No VAT", 0), -10);
            Assert.AreEqual((decimal)11, surchargedTransaction.GrossAmount);
        }

        [TestMethod]
        public void ATransactionCanHaveTaxes()
        {
            Transaction transaction = new Transaction("Nice Blue Cap", 1, 10, new Tax("5% VAT", 5), 0);
            Assert.AreEqual((decimal)10, transaction.GrossAmount);
            Assert.AreEqual((decimal)10.5, transaction.NetAmount);
            Assert.AreEqual((decimal)0.5, transaction.TaxAmount);
        }

        [TestMethod]
        public void RoundingTaxIsWellCalculatedThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("6% VAT is the same than 10% VAT!", 1, 0.1, new Tax("6% VAT", 6), 0);
            Assert.AreEqual((decimal)0.01, transaction.TaxAmount);
        }

        [TestMethod]
        public void RoundingTaxIsWellCalculatedThirdDecimalIsFive()
        {
            Transaction transaction = new Transaction("5% VAT is the same than 10% VAT!", 1, 0.1, new Tax("5% VAT", 5), 0);
            Assert.AreEqual((decimal)0.01, transaction.TaxAmount);
        }

        [TestMethod]
        public void RoundingTaxIsWellCalculatedThirdDecimalIsBelowFive()
        {
            Transaction transaction = new Transaction("1% VAT is NO VAT!", 1, 0.1, new Tax("4% VAT", 4), 0);
            Assert.AreEqual((decimal)0, transaction.TaxAmount);
        }

        [TestMethod]
        public void TaxIsAppliedPerUnitNotToTotal()
        {
            Transaction aSingleTwoCentsProduct = new Transaction("6% VAT is the same than 10% VAT!", 1, 0.2, new Tax("6% VAT", 6), 0);
            Transaction twoOneCentsProducts = new Transaction("6% VAT is the same than 10% VAT!", 2, 0.1, new Tax("6% VAT", 6), 0);
            Assert.AreEqual((decimal)0.01, aSingleTwoCentsProduct.TaxAmount);
            Assert.AreEqual((decimal)0.02, twoOneCentsProducts.TaxAmount);
        }

        [TestMethod]
        public void TaxIsAppliedAfterDiscount()
        {
            Transaction transaction = new Transaction("Ooops... it's for free!", 1, 0.01, new Tax("100% VAT", 100), 51);
            Assert.AreEqual((decimal)0, transaction.TaxAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TransactionsCantHaveNegativeCost()
        {
            try
            {
                Transaction transaction = new Transaction("A gift?! For me?!", 1, -100, new Tax("No VAT", 0), 0);

            }
            catch (ArgumentOutOfRangeException exception)
            {
                string[] exceptionMessages = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual("Transactions units cost can't be negative", exceptionMessages[0]);
                throw exception;
            }
        }

        [TestMethod]
        public void TransactionsCanHaveNegativeUnitNumbersToReflectDevolutionsOrCancellationsOnAmendingInvoices()
        {
            Transaction transaction = new Transaction("This product is defective. Return me my money!", -1, 50, new Tax("0% VAT", 0), 0);
            Assert.AreEqual((decimal)-1, transaction.Units);
        }

        [TestMethod]
        public void TheTotalAmoutOfATransacationCanBeNegative()
        {
            Transaction transaction = new Transaction("This product is defective. Return me my money!", -1, 50, new Tax("0% VAT", 0), 0);
            Assert.AreEqual((decimal)-50, transaction.NetAmount);
        }

        [TestMethod]
        public void RoundingNegativeTransactionsIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsBelowFive()
        {
            Transaction transaction = new Transaction("This product is defective. Return me my money!", -1, 9.994, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)-9.99, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingNegativeTransactionsIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsFive()
        {
            Transaction transaction = new Transaction("This product is defective. Return me my money!", -1, 9.995, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)-10, transaction.GrossAmount);
        }

        [TestMethod]
        public void RoundingNegativeTransactionsIsAllwaysTwoDecimalDigitsAwayFromZero_ExampleThirdDecimalIsAboveFive()
        {
            Transaction transaction = new Transaction("This product is defective. Return me my money!", -1, 9.996, new Tax("No VAT", 0), 0);
            Assert.AreEqual((decimal)-10, transaction.GrossAmount);
        }

        [TestMethod]
        public void TheTaxAmountOfANegativeGrossAmountIsNegative()
        {
            Transaction transaction = new Transaction("This product is defective. Return me my money!", -1, 10, new Tax("5% VAT", 5), 0);
            Assert.AreEqual((decimal)-0.5, transaction.TaxAmount);
        }

        [TestMethod]
        public void TheAmountOfReturningOneProductIsExactlyTheSameThanBuyinOneProductButnegative_CheckNoTaxNorDiscount()
        {
            Transaction transaction = new Transaction("Return me exactly the same I paid!", -1, 10, new Tax("5% VAT", 5), 0);
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheAmountOfReturningOneProductIsExactlyTheSameThanBuyinOneProductButnegative_CheckWithTax()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheAmountOfReturningOneProductIsExactlyTheSameThanBuyinOneProductButnegative_CheckWithDiscount()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public void TheAmountOfReturningOneProductIsExactlyTheSameThanBuyinOneProductButnegative_CheckWithTaxAndDiscount()
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

        [TestMethod]
        public void TheTransactionsCostAndTaxesCanDiferFromDefaultProductCost()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheTransactionsCostAndTaxesCanDiferFromDefaultServiceCost()
        {
            Assert.Inconclusive();
        }
    }
}
