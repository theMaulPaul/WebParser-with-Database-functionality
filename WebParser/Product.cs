using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebParser
{
    public class Product
    {
        [JsonPropertyName("productVendor")]
        public string Vendor { get; set; }

        [JsonPropertyName("productName")]
        public string Title { get; set; }

        [JsonPropertyName("productPartNumber")]
        public string Code { get; set; }

        [JsonPropertyName("productDetails")]
        public string Description { get; set; }

        [JsonPropertyName("retailPrice")]
        public string Price { get; set; }

        [JsonPropertyName("statusMessage")]
        public string Availability { get; set; }

        [JsonPropertyName("longURL")]
        public string Url { get; set; }

        public string ProductURL
        {
            get
            {
                return $"https://www.swansonvitamins.com/{Url}";
            }
        }
        public string ImageUrl
        {
            get
            {
                return $"https://media.swansonvitamins.com/images/items/master/{Code}.jpg";
            }
        }
    }
}
