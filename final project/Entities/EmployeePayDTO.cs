using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeePayDTO
    {
        public string? EmpId { get; set; }
        public int? Position { get; set; }
        public DateTime? DateHire { get; set; }
        public int? Payrate { get; set; }
        public DateTime? DateLastRaise { get; set; }
        public int? Salary { get; set; }
        public int? Bonus { get; set; }
        public string?EmpLName { get; set; }
        public string EmpFName { get; set; }
    }
}
