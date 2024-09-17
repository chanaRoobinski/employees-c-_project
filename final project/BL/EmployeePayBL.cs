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
    public class EmployeePayBL
    {
        public EmployeePayDTO convertEmpPayToDTO(EmployeePayTbl emp)
        {
            EmployeePayDTO empDTO = new EmployeePayDTO();
            empDTO.Salary = emp.Salary;
            empDTO.Payrate= emp.Payrate;
            empDTO.DateHire= emp.DateHire;
            empDTO.DateLastRaise= emp.DateLastRaise;
            empDTO.Bonus= emp.Bonus;
            empDTO.EmpId= emp.EmpId;
            empDTO.EmpFName = emp.Emp.FirstName;
            empDTO.EmpLName= emp.Emp.LastName;
            return empDTO;
        }

        public List<EmployeePayDTO> convertEmpPayListToDTO(List<EmployeePayTbl> l)
        {
            List<EmployeePayDTO> list = new List<EmployeePayDTO>();
            foreach(EmployeePayTbl e in l)
            {
                list.Add(convertEmpPayToDTO(e));
            }
            return list;
        }

        public EmployeePayTbl convertToEmployeePayTbl(EmployeePayDTO empDT)
        {
           EmployeePayTbl employeePayTbl = new EmployeePayTbl();
            employeePayTbl.Salary = empDT.Salary;
            employeePayTbl.Payrate= empDT.Payrate;
            employeePayTbl.DateHire= empDT.DateHire;
            employeePayTbl.DateLastRaise= empDT.DateLastRaise;
            employeePayTbl.Bonus= empDT.Bonus;
            employeePayTbl.EmpId= empDT.EmpId;
            employeePayTbl.Position= empDT.Position;
            
            //employeePayTbl.Emp.FirstName = empDT.EmpFName;
            //employeePayTbl.Emp.LastName = empDT.EmpLName;
            return employeePayTbl;
        }

        EmployeePayDAL E = new EmployeePayDAL();

        public async Task<List<EmployeePayDTO>> selectAllAsync()
        {
            return convertEmpPayListToDTO(await E.selectAllAsync());
        }

        public async Task<EmployeePayDTO> selectById(string id)
        {
            return convertEmpPayToDTO((await E.selectAllAsync()).
                FirstOrDefault(e=>e.EmpId==id));

        }

        public async Task<int> updateAsync(EmployeePayDTO c)
        {
            return await E.updateAsync(convertToEmployeePayTbl(c));
        }

        public Task<int> addAsync(EmployeePayDTO c)
        {
            return E.addAsync(convertToEmployeePayTbl(c));
        }
    }
}
