using Entities;
using Microsoft.AspNetCore.Mvc;
using BL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        CustomerBL CBl = new CustomerBL();

        [HttpGet]
        public async Task<List<CustomerDTO>> GET()
        {
            return await CBl.selectAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<CustomerDTO> GetByID(int id)
        {
            return await CBl.selectByIdAsync(id);
        }

        [HttpPut]
        public async Task<int> PUT([FromBody] CustomerDTO c) 
        {
            return await CBl.updateAsync(c);
        }
        [HttpPost]
        public async Task<int> ADD([FromBody] CustomerDTO c)
        {
            return await CBl.addAsync(c);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await CBl.deleteAsync(id);
        }
    }
}

