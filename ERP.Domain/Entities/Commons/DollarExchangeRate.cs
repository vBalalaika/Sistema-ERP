using Newtonsoft.Json;
using System;

namespace ERP.Domain.Entities.Commons
{
    public class DollarExchangeRate
    {
        public int Id { get; set; }

        [JsonProperty("fecha")]
        public DateTime? Date { get; set; }

        [JsonProperty("compra")]
        public decimal PurchasePrice { get; set; }

        [JsonProperty("venta")]
        public decimal SalePrice { get; set; }
    }
}
