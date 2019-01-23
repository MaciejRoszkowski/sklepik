namespace sklepik.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopDB : DbContext
    {
        public ShopDB()
            : base("name=baza")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public DbSet<Product> Product { get; set; }
        
    }
}
