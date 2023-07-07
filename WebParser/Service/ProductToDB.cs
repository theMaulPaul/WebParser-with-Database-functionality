using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.Models;

namespace WebParser.Service
{
    public class ProductToDB
    {
        private ProductsContext _context;

        public ProductToDB(ProductsContext context)
        {
            _context = context;
        }

        public ProductContext? SearchByCode(string code)
        {
            return _context.Products.FirstOrDefault(p => p.Code == code);
        }

        public void UpdateDB(List<Product> products)
        {
            products.ForEach(p =>
            {
                var productDataBase = SearchByCode(p.Code);
                if (productDataBase == null)
                {
                    productDataBase = new ProductContext();
                    productDataBase.CreatedAt = DateTime.Now;
                    _context.Add(productDataBase);
                }
                productDataBase.Availability = p.Availability == "In stock";
                productDataBase.Title = p.Title;
                productDataBase.Description = p.Description;
                productDataBase.Code = p.Code;
                productDataBase.Price = Decimal.Parse(p.Price, CultureInfo.InvariantCulture);
                productDataBase.Vendor = p.Vendor;
                productDataBase.ImageUrl = p.ImageUrl;
                productDataBase.UpdatedAt = DateTime.Now;
            });
            _context.SaveChanges();
        }
    }
}
