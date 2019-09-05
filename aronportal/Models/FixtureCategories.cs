using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class FixtureCategories
    {
        [Key]
        public int Id { get; set; }
        public string FixtureCategoryName { get; set; }
        public string FixtureCategoryCode { get; set; }

        public virtual ICollection<Fixtures> Fixtures { get; set; }
    }
}