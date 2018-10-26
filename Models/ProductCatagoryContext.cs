using Microsoft.EntityFrameworkCore;

namespace Products_and_Catagories.Models
{
    public class ProductCatagoryContext : DbContext
    {
        public ProductCatagoryContext(DbContextOptions<ProductCatagoryContext> options) : base(options) { }
        
        public DbSet<Product> Products {get;set;}
        public DbSet<Catagory> Catagories {get;set;}
        public DbSet<Association> Associations {get;set;}
    }
}