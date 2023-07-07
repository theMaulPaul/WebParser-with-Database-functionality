using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.Models
{
    public class ProductsContext: DbContext
    {
        public DbSet<ProductContext> Products { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }
    }
}
