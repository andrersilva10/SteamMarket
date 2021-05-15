using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ItemInventario
    {
        public int AppId { get; set; }
        public string ClassId { get; set; }
        public string InstanceId { get; set; }
        public int Currency { get; set; }
        public string BackgroundColor { get; set; }
        public string IconUrl { get; set; }
        public string IconUrlLarge { get; set; }
        public string Name { get; set; }
        [JsonProperty("name_color")]
        public string NameColor { get; set; }
        [JsonProperty("market_name")]
        public string MarketName { get; set; }
        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }
        [JsonProperty("lowest_price")]
        public decimal LowestPrice { get; set; }
        [JsonProperty("median_price")]
        public decimal MedianPrice { get; set; }
        public int Volume { get; set; }
    }
}
