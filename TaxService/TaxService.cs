using System;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Model;

namespace TaxService
{
    //Note: Ideally I would use business specific data model for this. But using same model 
    //which are consume by TaxCalculator.
    public class TaxService : ITaxService
    {
        private readonly ITaxCalculator _taxCalculator;

        
        public TaxService(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }
        public Task<double?> CalculateTaxAsync(Order order)
        {
            //Note:  business logic goes here
            return _taxCalculator.CalculateTaxAsync(order);
        }

        public Task<TaxRate> GetTaxRatesAsync(int ZipCode)
        {
            //Note:  business logic goes here
            return _taxCalculator.GetTaxRatesAsync(ZipCode);
        }
    }
}
