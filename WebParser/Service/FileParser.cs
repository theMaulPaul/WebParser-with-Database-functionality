using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebParser.Models;

namespace WebParser.Service
{
    public class FileParser
    {
        public List<Product> ParseFile(string fileURL)
        {
            int cnt = 1;

            var products = new List<Product>();

            while (true)
            {
                using (var client = new HttpClient())
                {
                    var link = fileURL + "/q?page=" + cnt.ToString();

                    var html = client.GetStringAsync(link).Result;
                    var expresion = @"adobeRecords"":(.+),""topProduct";
                    var matches = Regex.Matches(html, expresion);

                    if (matches.Count > 0)
                    {
                        var json = matches[0].Groups[1].Value;
                        var pageProducts = JsonSerializer.Deserialize<List<Product>>(json);
                        products.AddRange(pageProducts);
                        if(pageProducts.Count == 0)
                        {
                            Console.WriteLine("Parsing process is done...\n");
                            break;
                        }
                        ++cnt;
                    }
                }
            }
            return products;
        }
    }
}
