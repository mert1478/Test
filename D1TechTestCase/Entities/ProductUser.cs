using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Entities
{
    public class ProductUser
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
