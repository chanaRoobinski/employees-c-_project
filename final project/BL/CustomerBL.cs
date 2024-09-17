using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class CustomerBL
    {
        //DTOל CustomerTblהמרה מ 
        public CustomerDTO convertCustToDTO(CustomerTbl custTbl)
        {
            CustomerDTO cusDTO= new CustomerDTO();  
            cusDTO.CustAddress = custTbl.CustAddress;
            cusDTO.CustCity = custTbl.CustCity; 
            cusDTO.CustFax= custTbl.CustFax;    
            cusDTO.CustPhone= custTbl.CustPhone;
            cusDTO.CustId= custTbl.CustId;
            cusDTO.CustName= custTbl.CustName;
            cusDTO.EmpId= custTbl.EmpId;
            cusDTO.EmpFName = custTbl.Emp == null ? "" : custTbl.Emp.FirstName;
            cusDTO.EmpLName = custTbl.Emp == null ? "" : custTbl.Emp.LastName;
            return cusDTO;  
        }
        //DTOל CustomerTb-המרת רשימה מ
        public List<CustomerDTO> convertCustListToDTO(List<CustomerTbl> list)
        {
            List<CustomerDTO> cusDTOList = new List<CustomerDTO>(); 
            foreach (CustomerTbl custTbl in list) 
            {
                cusDTOList.Add(convertCustToDTO(custTbl));
            }
            return cusDTOList;  
        }


        //customerTbl ל DTO המרה מ
        public CustomerTbl convertToCustomerTbl(CustomerDTO c)
        {
            CustomerTbl custTbl = new CustomerTbl();
            custTbl.CustId= c.CustId;
            custTbl.CustName= c.CustName;
            custTbl.CustAddress= c.CustAddress;
            custTbl.CustFax= c.CustFax;
            custTbl.CustCity= c.CustCity;
            custTbl.CustPhone= c.CustPhone;
            custTbl.EmpId = c.EmpId;

            return custTbl;
        }

        CustomerDAL customerDAL = new CustomerDAL();

        //שליפה כל הנתונים מטבלת לקוחות
        public async Task<List<CustomerDTO>> selectAllAsync()
        {
            return convertCustListToDTO(await customerDAL.selectAllAsync());
        }

        //ID שליפת לקוח לפי
        public async Task<CustomerDTO> selectByIdAsync(int id)
        {
            return convertCustToDTO((await customerDAL.selectAllAsync()).
                FirstOrDefault(c => c.CustId == id));
        }

        //עידכון לקוח
        public async Task<int> updateAsync(CustomerDTO c)
        {
            return await customerDAL.updateAsync(convertToCustomerTbl(c));
        }


        //הוספת לקוח
        public Task<int> addAsync(CustomerDTO c)
        {
            return  customerDAL.addAsync(convertToCustomerTbl(c));
        }

        
        public async Task<int> deleteAsync (int id)
        {
            return await customerDAL.deleteAsync(id);
        }
    }
}