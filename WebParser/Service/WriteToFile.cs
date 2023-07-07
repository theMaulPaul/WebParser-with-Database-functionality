using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.Models;

namespace WebParser.Service
{
    public interface WriteToFile
    {
        public void SaveAs(string fileName, List<Product> list);
    }
}
