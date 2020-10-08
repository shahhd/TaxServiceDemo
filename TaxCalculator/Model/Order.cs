using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxCalculator.Model
{
    public class Order
    {

        [JsonPropertyName("from_country")]
        public string FromCountry { get; set; }

        [JsonPropertyName("from_zip")]
        public string FromZip { get; set; }

        [JsonPropertyName("from_state")]
        public string FromState { get; set; }

        [JsonPropertyName("to_country")]
        public string ToCountry { get; set; }

        [JsonPropertyName("to_zip")]
        public string ToZip { get; set; }

        [JsonPropertyName("to_state")]
        public string ToState { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("shipping")]
        public double Shipping { get; set; }

        [JsonPropertyName("line_items")]
        public IEnumerable<LineItems> LineItems { get; set; }
    }

    public class LineItems
    {
        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public string UnitPrice { get; set; }

        [JsonPropertyName("product_tax_code")]
        public string ProductTaxCode { get; set; }
    }

    public class CalculatedOrderTax
    {
        [JsonPropertyName("tax")]
        public Tax Tax { get; set; }
    }
    public class Tax
    {

        [JsonPropertyName("order_total_amount")]
        public double TotalAmount { get; set; }
    }

}
