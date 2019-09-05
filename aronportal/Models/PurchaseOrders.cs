using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class PurchaseOrders
    {
        [Key]
        public int Id { get; set; }
        public string Ponumber { get; set; }
        public DateTime? PorecDate { get; set; }
        public DateTime? PoestShipDate { get; set; }
        public DateTime? PoactShipDate { get; set; }
        public decimal? Pocost { get; set; }
        public decimal? Poprice { get; set; }
        public bool? Pocompleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public short CustomerId { get; set; }
        public string Poname { get; set; }

        public virtual ICollection<Poparts> Poparts { get; set; }
        public virtual ICollection<Pofixtures> Pofixtures { get; set; }
    }
}