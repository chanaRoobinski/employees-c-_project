using System.Linq;
using DAL.models;
using Microsoft.EntityFrameworkCore;
namespace DAL;

public class CustomerDAL
{
   public async Task<List<CustomerTbl>> selectAllAsync()
    {
        Employee_dbContext Edb=new Employee_dbContext();
        return await Edb.CustomerTbls.Include(e => e.Emp).ToListAsync();
    }

    public async Task<int> updateAsync(CustomerTbl c)
    {
        int n = -1;
        using(var dbContext=new Employee_dbContext())
        {
            CustomerTbl cust=await dbContext.CustomerTbls.Include(e => e.Emp).
                FirstOrDefaultAsync(cu=>cu.CustId==c.CustId);
            if(cust!=null)
            {
            cust.CustName=c.CustName;
            cust.CustPhone=c.CustPhone;
            cust.CustFax=c.CustFax;
            cust.CustAddress=c.CustAddress;
            cust.CustCity=c.CustCity;
            cust.EmpId=c.EmpId;
            cust.Emp = c.Emp;
            }
            n=await dbContext.SaveChangesAsync();
        }
        return n;
    }

    public async Task<int> addAsync (CustomerTbl c)
    {
        int x = -1;
        using (Employee_dbContext employee_DbContext = new Employee_dbContext())
        {
           employee_DbContext.CustomerTbls.AddAsync(c);
            x=await employee_DbContext.SaveChangesAsync();
        }
         return x;  
    }

    public async Task<int> deleteAsync(int id)
    {
        int x = -1;
        using(Employee_dbContext employee_DbContext = new Employee_dbContext())
        {
            CustomerTbl cus =await employee_DbContext.CustomerTbls.FirstOrDefaultAsync(c => c.CustId == id);
            if(cus != null)
            {
                employee_DbContext.CustomerTbls.Remove(cus);
                List<OrdersTbl> orders = (employee_DbContext.OrdersTbls.Where(order => order.CustId == cus.CustId)).ToList();
                foreach(OrdersTbl o in orders)
                    employee_DbContext.OrdersTbls.Remove(o);
            }
           x= employee_DbContext.SaveChanges();
        }
        return x;
    }

    public async Task<int> updateAfterRmoveEmp(CustomerTbl c)
    {
        int x = -1;
        using (Employee_dbContext employee_DbContext = new Employee_dbContext())
        {
            CustomerTbl cus = await employee_DbContext.CustomerTbls.FirstOrDefaultAsync(cu => cu.CustId == c.CustId);
            if (cus != null)
                cus.EmpId = null;
            x= employee_DbContext.SaveChanges();
        }
        return x;

    }
}