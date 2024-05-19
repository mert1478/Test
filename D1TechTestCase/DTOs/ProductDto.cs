using D1TechTestCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.DTOs
{
    public class ProductDto: BaseDto
    {
        public string Name { get; set; }
        public uint Stock { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ProductFeatureDto ProductFeature { get; set; } 
    }
}
