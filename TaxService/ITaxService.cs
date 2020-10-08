using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Model;

namespace TaxService
{
    public interface ITaxService
    {

        /// <summary>
        /// Get tax rate based on zipcodes
        /// </summary>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        Task<TaxRate> GetTaxRatesAsync(int ZipCode);

        /// <summary>
        /// Calculate tax and return final order amount with tax.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<double?> CalculateTaxAsync(Order order);
    }
}
