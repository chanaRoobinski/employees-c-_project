using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController
    {
        OrdersBL ord=new OrdersBL();
        [HttpGet]
        public async Task<List<OrdersDTO>> GET()
        {
            return await ord.selectAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<OrdersDTO> GetById(int id)
        {
            return await ord.selectById(id);
        }
        [HttpPut]
        public async Task<int> update([FromBody] OrdersDTO o)
        {
            return await ord.updateAsync(o);
        }

        [HttpPost]
        public async Task<int> ADD([FromBody] OrdersDTO o)
        {
            return await ord.addAsync(o);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await ord.delete(id);
        }
    }
}
