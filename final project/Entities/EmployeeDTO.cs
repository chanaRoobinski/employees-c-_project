using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeeDTO
    {
        public string EmpId { get; set; } = null!;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
