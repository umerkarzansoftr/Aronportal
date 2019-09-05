using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class Pofixtures
    {
        [Key]
        public int Id { get; set; }
        public int? PurchaseOrderId { get; set; }
        public int? FixtureId { get; set; }
        public decimal? FixturePrice { get; set; }
        public decimal? FixtureCommision { get; set; }
        public int? FixtureQuantity { get; set; }
        public int? FixtureQuantityCompleted { get; set; }
        public int? FixtureQuantityShipped { get; set; }
        public decimal? FixtureCost { get; set; }

        public virtual Fixtures Fixture { get; set; }
        public  virtual PurchaseOrders PurchaseOrder { get; set; }
    }
}