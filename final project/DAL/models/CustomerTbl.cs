using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class CustomerTbl
    {
        public CustomerTbl()
        {
            OrdersTbls = new HashSet<OrdersTbl>();
        }

        public int CustId { get; set; }
        public string? CustName { get; set; }
        public string? CustAddress { get; set; }
        public string? CustCity { get; set; }
        public string? CustPhone { get; set; }
        public string? CustFax { get; set; }
        public string? EmpId { get; set; }

        public virtual EmployeeTbl? Emp { get; set; }
        public virtual ICollection<OrdersTbl> OrdersTbls { get; set; }
    }
}
