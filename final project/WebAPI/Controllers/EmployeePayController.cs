using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePayController
    {
        EmployeePayBL EmpBL = new EmployeePayBL();

        [HttpGet]
        public async Task<List<EmployeePayDTO>> GET()
        {
            return await EmpBL.selectAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<EmployeePayDTO> GetById(string id)
        {
            return await EmpBL.selectById(id);
        }
        [HttpPut]
        public async Task<int> update( [FromBody] EmployeePayDTO emp)
        {
            return await EmpBL.updateAsync(emp);
        }

        [HttpPost]
        public async Task<int> ADD([FromBody] EmployeePayDTO ep)
        {
            return await EmpBL.addAsync(ep);
        }
    }
}
