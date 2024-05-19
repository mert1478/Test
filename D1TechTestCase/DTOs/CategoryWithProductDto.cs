using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.DTOs
{
    public class CategoryWithProductDto
    {
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
