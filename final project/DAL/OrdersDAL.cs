using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class OrdersDAL
    {
        public async Task<List<OrdersTbl>> SelectAllAsync()
        {
            Employee_dbContext _dbContext=new Employee_dbContext();
            return await _dbContext.OrdersTbls.Include(e => e.Cust ).Include(e=> e.Prod) .ToListAsync();
        }
       
        public async Task<int> updateAsync(OrdersTbl o)
        {
            int x = -1;
            using(var dbC = new Employee_dbContext())
            {
                OrdersTbl ord=await dbC.OrdersTbls.
                    FirstOrDefaultAsync(or=>or.OrdNum==o.OrdNum);
                if(ord!=null)
                { 
                ord.OrdDate = o.OrdDate;
                ord.Qty=o.Qty;
                ord.CustId=o.CustId;    
                ord.Cust=o.Cust;
                ord.ProdId=o.ProdId;
                ord.Prod=o.Prod;
                }
                x=dbC.SaveChanges();
                
            }
            return x;
        }

        public async Task<int> addAsync(OrdersTbl o)
        {
            int x = -1;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                employee_DbContext.OrdersTbls.AddAsync(o);
                x = await employee_DbContext.SaveChangesAsync();
            }
            return x;
        }

        public async Task<int> deleteAsync(int id)
        {
            int x = -1;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                OrdersTbl ord = await employee_DbContext.OrdersTbls.FirstOrDefaultAsync(o => o.OrdNum == id);
                if (ord!= null)
                {
                    employee_DbContext.OrdersTbls.Remove(ord);


                }
                x = employee_DbContext.SaveChanges();
            }
            return x;
        }
    }
}
