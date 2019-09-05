
using System.Data.Entity;

namespace aronportal.Models
{
    public class AronPortalDbContext : DbContext
    {
        public AronPortalDbContext()
            : base("name=AronPortalDbContext")
        {
        }
        public virtual DbSet<FixtureCategories> FixtureCategories { get; set; }
        public virtual DbSet<FixtureParts> FixtureParts { get; set; }
        public virtual DbSet<Pofixtures> Pofixtures { get; set; }
        public virtual DbSet<Poparts> Poparts { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<Fixtures> Fixtures { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategory { get; set; }
        public virtual DbSet<ItemGroups> ItemGroups { get; set; }
        public virtual DbSet<PurchaseOrders> PurchaseOrders { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        
    }
}