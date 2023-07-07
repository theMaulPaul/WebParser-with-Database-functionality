using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.Models;

namespace WebParser.Service
{
    public class SaveToXLS: WriteToFile
    {
        public void SaveAs(string fileName, List<Product> list)
        {
            using (var book = new XLWorkbook())
            {
                var sheet = book.AddWorksheet("Products list");

                sheet.Cell("A1").Value = "Availability";
                sheet.Cell("B1").Value = "Title";
                sheet.Cell("C1").Value = "Description";
                sheet.Cell("D1").Value = "Code";
                sheet.Cell("E1").Value = "Price";
                sheet.Cell("F1").Value = "Vendor";
                sheet.Cell("G1").Value = "URL-image";
                sheet.Cell("H1").Value = "Product URL";

                int row = 2;
                foreach (var item in list)
                {
                    sheet.Cell($"A{row}").Value = item.Availability;
                    sheet.Cell($"B{row}").Value = item.Title;
                    sheet.Cell($"C{row}").Value = item.Description;
                    sheet.Cell($"D{row}").Value = item.Code;
                    sheet.Cell($"E{row}").Value = item.Price;
                    sheet.Cell($"F{row}").Value = item.Vendor;
                    sheet.Cell($"G{row}").Value = item.ImageUrl;
                    sheet.Cell($"H{row}").Value = item.ProductURL;

                    ++row;
                }

                book.SaveAs(fileName);
                Console.WriteLine("Writing to Excel file done...\n");
            }
        }
        
        public void SaveAs(string fileName, List<ProductContext> list)
        {
            using (var book = new XLWorkbook())
            {
                var sheet = book.AddWorksheet("Products list");

                sheet.Cell("A1").Value = "Availability";
                sheet.Cell("B1").Value = "Title";
                sheet.Cell("C1").Value = "Description";
                sheet.Cell("D1").Value = "Code";
                sheet.Cell("E1").Value = "Price";
                sheet.Cell("F1").Value = "Vendor";
                sheet.Cell("G1").Value = "URL-image";
                sheet.Cell("H1").Value = "Product URL";

                int row = 2;
                foreach (var item in list)
                {
                    sheet.Cell($"A{row}").Value = item.Availability;
                    sheet.Cell($"B{row}").Value = item.Title;
                    sheet.Cell($"C{row}").Value = item.Description;
                    sheet.Cell($"D{row}").Value = item.Code;
                    sheet.Cell($"E{row}").Value = item.Price;
                    sheet.Cell($"F{row}").Value = item.Vendor;
                    sheet.Cell($"G{row}").Value = item.ImageUrl;
                    sheet.Cell($"H{row}").Value = item.ProductUrl;

                    ++row;
                }

                book.SaveAs(fileName);
                Console.WriteLine("Writing to Excel file done...\n");
            }
        }
    }
}
