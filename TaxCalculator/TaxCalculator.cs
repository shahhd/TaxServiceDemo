using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Model;

namespace TaxCalculator
{

    //Note: Ideally I would create generic data model for this. But using same model 
    //which are consume by TaxHttpClient
    public class TaxCalculator : ITaxCalculator
    {
        private readonly ITaxHttpClient _taxHttpClient;

        public TaxCalculator(ITaxHttpClient taxHttpClient)
        {
            _taxHttpClient = taxHttpClient;
        }

        public Task<double?> CalculateTaxAsync(Order order)
        {
            // Any data massaging logic to will come here
            return _taxHttpClient.CalculateTaxAsync(order);
        }

        public Task<TaxRate> GetTaxRatesAsync(int ZipCode)
        {
            // Any data massaging logic to will come here
            return _taxHttpClient.GetTaxRatesAsync(ZipCode);
        }
    }
}
