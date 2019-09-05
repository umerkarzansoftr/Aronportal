using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class Vendors
    {
        [Key]
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string VendorNumber { get; set; }
    }
}