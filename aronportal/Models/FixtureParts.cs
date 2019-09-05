using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class FixtureParts
    {
        [Key]
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public int? Cost { get; set; }

        public virtual Parts Parts { get; set; }
        public virtual Fixtures Fixtures { get; set; }
    }
}