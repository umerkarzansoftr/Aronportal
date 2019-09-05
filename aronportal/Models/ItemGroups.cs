using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class ItemGroups
    {
        [Key]
        public int Id { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemGroupCode { get; set; }
        public virtual ICollection<Parts> Parts { get; set; }
    }
}