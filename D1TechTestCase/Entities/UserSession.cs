using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Entities
{
    public class UserSession: BaseEntity
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string? Browser { get; set; }
        public string? OS { get; set; }
        public DateTime Expiration { get; set; }
    }
}
