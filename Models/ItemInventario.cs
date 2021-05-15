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
        public string NameColor { get; set; }
        public string MarketName { get; set; }
        public string MarketHashName { get; set; }
    }
}
