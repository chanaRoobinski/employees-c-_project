using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class EmployeeTbl
    {
        public EmployeeTbl()
        {
            CustomerTbls = new HashSet<CustomerTbl>();
        }

        public string EmpId { get; set; } = null!;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        public virtual ICollection<CustomerTbl> CustomerTbls { get; set; }
    }
}
