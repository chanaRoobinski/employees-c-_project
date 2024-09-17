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
    public class EmployeeBL
    {
        public EmployeeDTO convertEmpToDTO(EmployeeTbl empTbl)
        {
            EmployeeDTO empD = new EmployeeDTO();
            empD.Zip = empTbl.Zip;
            empD.FirstName = empTbl.FirstName;
            empD.LastName = empTbl.LastName;
            empD.Phone= empTbl.Phone;
            empD.City= empTbl.City;
            empD.Address= empTbl.Address;
            empD.EmpId= empTbl.EmpId;
            return empD;
        }

        public List<EmployeeDTO> convertEmpListToDTO(List<EmployeeTbl> l)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();   
            foreach(EmployeeTbl emp in l)
            {
                list.Add(convertEmpToDTO(emp));
            }
            return list;
        }

        public EmployeeTbl convertToEmployeeTbl(EmployeeDTO empD)
        {
            EmployeeTbl employeeTbl = new EmployeeTbl();
            employeeTbl.Zip = empD.Zip;
            employeeTbl.FirstName = empD.FirstName;
            employeeTbl.LastName = empD.LastName;
            employeeTbl.Address= empD.Address;
            employeeTbl.EmpId= empD.EmpId;
            employeeTbl.City= empD.City;
            employeeTbl.Phone= empD.Phone;
            return employeeTbl;
        }
        EmployeeDAL EDal=new EmployeeDAL();

        public async Task<List<EmployeeDTO>> selectAllAsync()
        {
            return convertEmpListToDTO(await EDal.selectAllAsync());
        }

        public async Task<EmployeeDTO> selectById(string id)
        {
            return convertEmpToDTO((await EDal.selectAllAsync()).
                FirstOrDefault(e => e.EmpId == id));
        }

        public async Task<int> updateAsync(EmployeeDTO c)
        {
            return await EDal.updateAsync(convertToEmployeeTbl(c));
        }

        public Task<int> addAsync(EmployeeDTO c)
        {
            return EDal.addAsync(convertToEmployeeTbl(c));
        }

        public Task<int> delete(String id)
        {
            return EDal.deleteAsync(id);
        }
    }
}
