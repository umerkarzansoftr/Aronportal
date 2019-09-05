using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class Poparts
        
    {
        [Key]
        public int Id { get; set; }
        public int? PartId { get; set; }
        public int? Quantity { get; set; }
        public int PurchaseOrderId { get; set; }

        public virtual Parts Part { get; set; }
        public virtual PurchaseOrders PurchaseOrder { get; set; }
    }
}