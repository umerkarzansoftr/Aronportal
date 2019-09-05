using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aronportal.DTOS
{
    public class PartsDTO
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string PartDescription { get; set; }
        public string PartVendor { get; set; }
        public decimal? PartCost { get; set; }
        public int? PartQuantity { get; set; }
        public string InventoryCategory { get; set; }
        public string ItemGroupId { get; set; }
        public string PartNumber { get; set; }
    }
}
