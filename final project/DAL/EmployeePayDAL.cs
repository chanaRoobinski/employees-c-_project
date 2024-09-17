using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class EmployeePayDAL
    {
        public async Task< List<EmployeePayTbl>> selectAllAsync()
        {
            Employee_dbContext Edb = new Employee_dbContext();
            return await Edb.EmployeePayTbls.Include(e => e.Emp).ToListAsync();
        }

        public async Task<int> updateAsync(EmployeePayTbl e)
        {
            int x = -1;
            using(var dbC = new Employee_dbContext())
            {
                EmployeePayTbl emp = await dbC.EmployeePayTbls.Include(e => e.Emp).FirstOrDefaultAsync(em => em.EmpId == e.EmpId);
                if (emp != null)
                {
                    emp.Position = e.Position;
                    emp.Bonus = e.Bonus;
                    emp.Salary = e.Salary;
                    emp.DateLastRaise = e.DateLastRaise;
                    emp.DateHire = e.DateHire;
                    emp.Payrate = e.Payrate;
                }
                x=dbC.SaveChanges();
            }
            return x;
        }

        public async Task<int> addAsync(EmployeePayTbl ep)
        {
            int x = -1;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                employee_DbContext.EmployeePayTbls.AddAsync(ep);
                x = await employee_DbContext.SaveChangesAsync();
            }
            return x;
        }
        
    }
}
