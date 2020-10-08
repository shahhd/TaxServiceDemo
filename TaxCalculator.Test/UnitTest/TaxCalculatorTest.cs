using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculator.Model;
using Xunit;

namespace TaxCalculator.Test.UnitTest
{
    public class TaxCalculatorTest
    {
        [Fact]
        public void GetTaxRatesAsync_Mock_TaxHttpClient()
        {
            //arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"rate\": {\"state_rate\": \"0.006\"}}", Encoding.UTF8, "application/json")
                });

            var client = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var logging = new Logging();
            var taxHttpClient = new TaxHttpClient(mockFactory.Object, logging);
            var taxCalculator = new TaxCalculator(taxHttpClient);

            //act
            var data = taxCalculator.GetTaxRatesAsync(20105);


            //Assert
            Assert.False(data.IsFaulted);
            Assert.True(data.IsCompletedSuccessfully);
            mockMessageHandler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req =>
      req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());

        }
        [Fact]
        public void CalculateTaxAsync_Mock_TaxHttpClient()
        {
            //arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"tax\": {\"order_total_amount\": 11}}", Encoding.UTF8, "application/json")
                });

            var client = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var logging = new Logging();
            var taxHttpClient = new TaxHttpClient(mockFactory.Object, logging);
            var taxCalculator = new TaxCalculator(taxHttpClient);
            var order = new Order();

            //act
            var data = taxCalculator.CalculateTaxAsync(order);


            //Assert
            Assert.False(data.IsFaulted);
            Assert.True(data.IsCompletedSuccessfully);
            mockMessageHandler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req =>
      req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());

        }
    }
}
