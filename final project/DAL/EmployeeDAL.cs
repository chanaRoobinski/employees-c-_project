using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class EmployeeDAL
    {
        public async Task<List<EmployeeTbl>> selectAllAsync()
        {
            Employee_dbContext Edb = new Employee_dbContext();
            return await Edb.EmployeeTbls.ToListAsync();
        }

        public async Task<int> updateAsync(EmployeeTbl e)
        {
            int x = -1;
            using(Employee_dbContext edb=new Employee_dbContext())
            {
               EmployeeTbl emp=await edb.EmployeeTbls.FirstOrDefaultAsync(em=>em.EmpId==e.EmpId);

                if(emp!=null)
                {
                emp.FirstName = e.FirstName;
                emp.LastName = e.LastName;
                emp.Address= e.Address;
                emp.City = e.City;
                emp.Zip = e.Zip;
                emp.Phone= e.Phone;

                }
               
                x=await edb.SaveChangesAsync();
            }
            return x;
        }

        public async Task<int> addAsync(EmployeeTbl e)
        {
            int x = -1;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                employee_DbContext.EmployeeTbls.AddAsync(e);
                x = await employee_DbContext.SaveChangesAsync();
            }
            return x;
        }

        public async Task<int> deleteAsync(string id)
        {
            int x = 0;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                EmployeeTbl emp = await employee_DbContext.EmployeeTbls.FirstOrDefaultAsync(e => e.EmpId == id);
                if (emp != null)
                {
                    //remove employee from db
                    employee_DbContext.EmployeeTbls.Remove(emp);

                    //remove from employeePay 
                    EmployeePayTbl empPay = await employee_DbContext.EmployeePayTbls.FirstOrDefaultAsync(e => e.EmpId == id);
                    if(empPay != null)
                        employee_DbContext.EmployeePayTbls.Remove(empPay);

                    //update customer
                    CustomerDAL cDal= new CustomerDAL();
                    List<CustomerTbl> custs=await employee_DbContext.CustomerTbls.Where(c=>c.EmpId==id).ToListAsync();
                    foreach(CustomerTbl cust in custs)
                        x+=await cDal.updateAfterRmoveEmp(cust);
                }
                x += employee_DbContext.SaveChanges();
            }
            return x;
        }
    }
}
