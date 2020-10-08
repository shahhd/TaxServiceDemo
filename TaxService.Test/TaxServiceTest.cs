using Moq;
using System;
using TaxCalculator.Interface;
using TaxCalculator.Model;
using Xunit;
using TaxService;
using System.Threading.Tasks;

namespace TaxService.Test
{

    //Note: In real world, I would write more unit and integration test to cover all positive and negative scenario,
    //Below test case is for reference only to provide overall structure and design for test case.
    public class TaxServiceTest
    {
        [Fact]
        public async Task CalculateTaxAsync_Mock_TaxCalculator()
        {
            //arrange
            var mockTaxCalculator = new Mock<ITaxCalculator>();
            mockTaxCalculator.Setup(t => t.CalculateTaxAsync(It.IsAny<Order>())).ReturnsAsync(11);
            var taxService = new TaxService(mockTaxCalculator.Object);
            
            //act
            var data = await taxService.CalculateTaxAsync(new Order());

            //assert
            Assert.Equal(11,data.Value);
        }

        [Fact]
        public async Task GetTaxRatesAsync_Mock_TaxCalculator()
        {
            //arrange
            var mockTaxCalculator = new Mock<ITaxCalculator>();
            var mockTaxRate = new TaxRate();
            mockTaxRate.rate = new Rate();
            mockTaxRate.rate.county = "LA";
            mockTaxCalculator.Setup(t => t.GetTaxRatesAsync(It.IsAny<int>())).ReturnsAsync(mockTaxRate);
            var taxService = new TaxService(mockTaxCalculator.Object);

            //act
            var data = await taxService.GetTaxRatesAsync(20105);

            //assert
            Assert.Equal(mockTaxRate.rate.county, data.rate.county);
        }
    }
}
