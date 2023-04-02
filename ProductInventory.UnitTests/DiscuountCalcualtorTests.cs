using FluentAssertions;
using Product.Inventory.Logic;
using System.ComponentModel.DataAnnotations;
using Product.Inventory.Controllers;
using Xunit;

namespace ProductInventory.UnitTests
{
    public class DiscountCalculatorTests
    {
        private readonly DiscountCalculator _caculator;

        public DiscountCalculatorTests()
        {
            _caculator = new DiscountCalculator();
        }

        [Theory]
        [InlineData(101, 20, 10)]
        [InlineData(999, 10, 5)]
        [InlineData(1000000, 10, 5)]
        public void Given_CalculateDiscount_When_quantity_is_greater_than_discount_rule_value_Then_return_price_with_half_price_discount(int quantity, int price, decimal expectedResult)
        {
            var result = _caculator.CalculateDiscount(price, quantity);

            result.Should().Be(expectedResult);
        }
    }
}