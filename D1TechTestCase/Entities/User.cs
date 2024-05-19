using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Entities
{
    public class User: BaseEntity
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecurityStamp { get; set; }
        public virtual ICollection<Product> Favorites { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

    }
}
