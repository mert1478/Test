using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Entities
{
    public class Product: BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public uint Stock { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Category Category { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ProductFeature ProductFeature { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<User> Users { get; set; }
    }
}
