using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Model
{
    public class Rate
    {
        public string city_rate { get; set; }
        public string combined_rate { get; set; }
        public string country_rate { get; set; }
        public string county { get; set; }
        public string county_rate { get; set; }
        public string state_rate { get; set; }
    }

    public class TaxRate
    {
        public Rate rate { get; set; }
    }
}
