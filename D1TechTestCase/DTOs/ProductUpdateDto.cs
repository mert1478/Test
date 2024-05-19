using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.DTOs
{
    public class ProductUpdateDto: BaseDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public uint Stock { get; set; }
        public decimal Price { get; set; }
        public ProductFeatureDto ProductFeature { get; set; }
    }
}
