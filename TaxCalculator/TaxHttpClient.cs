using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Model;

namespace TaxCalculator
{
    public class TaxHttpClient : ITaxHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogging _logging;

        public TaxHttpClient(IHttpClientFactory clientFactory, ILogging logging)
        {
            _clientFactory = clientFactory;
            _logging = logging;
        }

        public async Task<TaxRate> GetTaxRatesAsync(int ZipCode)
        {
            
            //Basic zip code check
            if(ZipCode <=0)
            {
                _logging.Error($"TaxHttpClient failed for GetTaxRatesAsync, Reason : ZipCode {ZipCode} is not valid");
                return null;
            }
            var request = new HttpRequestMessage(HttpMethod.Get,string.Format("rates/{0}", ZipCode));
            var client = _clientFactory.CreateClient("taxJar");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await System.Text.Json.JsonSerializer.DeserializeAsync
                       <TaxRate>(responseStream);
            }
            else
                _logging.Error($"TaxHttpClient failed for GetTaxRatesAsync, Reason : {client.BaseAddress} call is not succeed");

            return null;

        }

        public async Task<double?> CalculateTaxAsync(Order order)
        {
            
            //Basic check
            if (order == null)
            {
                _logging.Error($"TaxHttpClient failed for CalculateTaxAsync, Reason : order data is null");
                return null;
            }
            var client = _clientFactory.CreateClient("taxJar");
            var jsonContent = System.Text.Json.JsonSerializer.Serialize<Order>(order);
            var contentData = new StringContent(jsonContent, System.Text.Encoding.UTF8,"application/json");
            var response = await client.PostAsync(client.BaseAddress +"taxes", contentData);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var data= await System.Text.Json.JsonSerializer.DeserializeAsync
                       <CalculatedOrderTax>(responseStream);
                if (data.Tax != null) return data.Tax.TotalAmount;
            }
            else
                _logging.Error($"TaxHttpClient failed for GetTaxRatesAsync, Reason : {client.BaseAddress} call is not succeed");

            return null;
        }
    }
}
