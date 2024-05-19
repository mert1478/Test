using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Entities
{
    public class ProductFeature: BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Product Product { get; set; }
    }
}
