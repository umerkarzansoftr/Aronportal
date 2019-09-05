using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class Parts
    {
        [Key]
        public int Id { get; set; }
        public string PartName { get; set; }
        public string PartDescription { get; set; }
        public string PartVendor { get; set; }
        public decimal? PartCost { get; set; }
        public int? PartQuantity { get; set; }
        public int? InventoryCategoryId { get; set; }
        public int? ItemGroupsId { get; set; }
        public string PartNumber { get; set; }
        public virtual InventoryCategory InventoryCategory { get; set; }
        public virtual ItemGroups ItemGroups { get; set; }
        public virtual ICollection<FixtureParts> FixtureParts { get; set; }
        public virtual ICollection<Poparts> Poparts { get; set; }
    }
}