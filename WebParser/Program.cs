using System.Text.Json;
using System.Text.RegularExpressions;
using WebParser;
using ClosedXML.Excel;
using WebParser.Models;
using WebParser.Service;

var webPage = "https://www.swansonvitamins.com/ncat1/Vitamins+and+Supplements/ncat2/Multivitamins/ncat3/Multivitamins+with+Iron";
var parser = new FileParser();
var list = parser.ParseFile(webPage);

WriteToFile transition = new SaveToXLS();
transition.SaveAs("productsList.xlsx", list);

using (var db = new ProductsContext())
{
    var s = new ProductToDB(db);
    s.UpdateDB(list);
    Console.WriteLine("Writing to datbase done...");
}

//products.ForEach(p =>
//{
//    Console.WriteLine($"{p.Number} - {p.Title} {p.Vendor} - {p.Price}");
//});public void SaveFile(string fileName, List<Product> products)




//using (var db = new ProductCntext())
//{
//    products.ForEach(p =>
//    {
//        var existingProduct = db.Products.FirstOrDefault(x => x.Code = p.Number);
//        if(existingProduct = null)
//        {
//            existingProduct = new Product();
//        }
//        existingProduct.Vendor = p.Vendor;
//        existingProduct.Name = p.Name;
//        existingProduct.Code = p.Code;
//        existingProduct.Details = p.Details;
//        existingProduct.Price = p.Price;
//        existingProduct.CreatedAt = p.Crea
//    });
//}




