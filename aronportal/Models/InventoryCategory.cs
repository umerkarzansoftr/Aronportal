using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class InventoryCategory
    {
        [Key]
        public int Id { get; set; }
        public string InventoryCategoryName { get; set; }
        public string InventoryCategoryCode { get; set; }
        
        public virtual ICollection<Parts> Parts { get; set; }
    }
}