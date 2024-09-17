using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        EmployeeBL EBl = new EmployeeBL();

        [HttpGet]
        public async Task<List<EmployeeDTO>> GET()
        {
            return await EBl.selectAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetById(string id)
        {
            return await EBl.selectById(id);
        }
        [HttpPut]
        public async Task<int> update( [FromBody] EmployeeDTO e)
        {
            return await EBl.updateAsync(e);
        }
        [HttpPost]
        public async Task<int> ADD([FromBody] EmployeeDTO e)
        {
            return await EBl.addAsync(e);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(string id)
        {
            return await EBl.delete(id);
        }
    }
}
