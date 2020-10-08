using System;
using System.Threading.Tasks;
using TaxCalculator.Model;
using TaxCalculator.Test.IntegrationTest;
using Xunit;

namespace TaxCalculator.Test
{
    public class TaxHttpClientTest : IClassFixture<IntegrationTestFixture>
    {

        private readonly IntegrationTestFixture _fixture;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fixture"></param>
        public TaxHttpClientTest(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Can_I_Get_Tax_Rate_By_ZipCode()
        {
            //arrange
            int zipCode = 20105;
            
            //act
           var data = await _fixture._taxhttpClient.GetTaxRatesAsync(zipCode);

            //assert
            Assert.NotEmpty(data.rate.county);
        }

        [Fact]
        public async Task Can_I_Get_Tax_Rate_By_Wrong_ZipCode()
        {
            //arrange
            int zipCode = -4;
            TaxRate data = new TaxRate();
            Rate r = new Rate();
            data.rate = r;
            data.rate.county = "dummy";
            
            //act
            data = await _fixture._taxhttpClient.GetTaxRatesAsync(zipCode);

            //assert
            Assert.Null(data);
        }

        [Fact]
        public async Task Can_I_CalculateTax_By_Order()
        {
            //arrange
            Order data = new Order();
            data.FromCountry = "US";
            data.FromZip = "92093";
            data.FromState = "CA";
            data.ToCountry = "US";
            data.ToZip = "92093";
            data.ToState = "CA";
            data.Amount = 20.45;
            data.Shipping = 7;



            //act
            var totalAmount = await _fixture._taxhttpClient.CalculateTaxAsync(data);

            //assert
            Assert.NotNull(totalAmount);
        }
    }
}
