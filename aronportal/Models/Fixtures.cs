using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aronportal.Models
{
    public class Fixtures
    {
        [Key]
        public int Id { get; set; }
        public string FixtureName { get; set; }
        public decimal? FixtureCost { get; set; }
        public string FixtureCode { get; set; }
        public int? FixtureCategoryId { get; set; }
        public virtual FixtureCategories FixtureCategories { get; set; }
        
        public virtual ICollection<FixtureParts> FixtureParts { get; set; }
        public virtual ICollection<Pofixtures> Pofixtures { get; set; }
    }
}