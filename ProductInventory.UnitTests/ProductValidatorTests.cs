using FluentAssertions;
using Product.Inventory.Logic;
using System.ComponentModel.DataAnnotations;
using Product.Inventory.Controllers;

namespace ProductInventory.UnitTests
{
    public class DiscuountCalcualtorTests
    {
        private readonly ProductValidator _validator;

        public DiscuountCalcualtorTests()
        {
            _validator = new ProductValidator();
        }


        [Fact]
        public void Given_ValidateProduct_When_Price_is_greaten_than_limit_Then_thrown_exception()
        {
           
        }
    }
}