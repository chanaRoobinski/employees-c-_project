using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class EmployeePayTbl
    {
        public string? EmpId { get; set; }
        public int? Position { get; set; }
        public DateTime? DateHire { get; set; }
        public int? Payrate { get; set; }
        public DateTime? DateLastRaise { get; set; }
        public int? Salary { get; set; }
        public int? Bonus { get; set; }

        public virtual EmployeeTbl? Emp { get; set; }
    }
}
