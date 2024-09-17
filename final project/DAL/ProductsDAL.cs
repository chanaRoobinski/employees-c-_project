using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ProductsDAL
    {
        public async Task<List<ProductsTbl>> selectAllAsync()
        {
            Employee_dbContext db=new Employee_dbContext();
            return await db.ProductsTbls.ToListAsync();
        }

        public async Task<int> updateAsync(ProductsTbl p)
        {
            int x = -1;
            using(var db = new Employee_dbContext())
            {
                ProductsTbl pTbl =await db.ProductsTbls.FirstOrDefaultAsync(pr=>pr.ProdId==p.ProdId);
                if(pTbl != null)
                { 
                pTbl.ProdDesc=p.ProdDesc;
                pTbl.Cost=p.Cost;
                }
                x=db.SaveChanges();
            }
            return x;   
        }

        public async Task<int> addAsync(ProductsTbl p)
        {
            int x = -1;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                employee_DbContext.ProductsTbls.AddAsync(p);
                x = await employee_DbContext.SaveChangesAsync();
            }
            return x;
        }

        public async Task<int> deleteAsync(int id)
        {
            int x = -1;
            using (Employee_dbContext employee_DbContext = new Employee_dbContext())
            {
                ProductsTbl prod = await employee_DbContext.ProductsTbls.FirstOrDefaultAsync(c => c.ProdId == id);
                if (prod != null)
                {
                    employee_DbContext.ProductsTbls.Remove(prod);
                    List<OrdersTbl> orders = (employee_DbContext.OrdersTbls.Where(order => order.ProdId == prod.ProdId)).ToList();
                    foreach (OrdersTbl o in orders)
                        employee_DbContext.OrdersTbls.Remove(o);
                }
                x = employee_DbContext.SaveChanges();
            }
            return x;
        }
    }
}
