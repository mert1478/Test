using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.DTOs
{
    public class UserSessionDto: BaseDto
    {
        public string? Browser { get; set; }
        public string? OS { get; set; }
    }
}
