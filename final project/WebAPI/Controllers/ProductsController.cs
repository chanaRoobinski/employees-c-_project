using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController
    {
     ProductsBL Pbl=new ProductsBL();
        [HttpGet]
        public async Task<List<ProductsDTO>> GET()
        {
            return await Pbl.selectAllAsync();
        }

        //public async Task<ProductsDTO> GetById(int id)
        //{
        //    return await Pbl.selectById(id);
        //}
        [HttpPut]
        public async Task<int> update([FromBody] ProductsDTO p)
        {
            return await Pbl.updateAsync(p);
        }

        [HttpPost]
        public async Task<int> ADD([FromBody] ProductsDTO p)
        {
            return await Pbl.addAsync(p);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await Pbl.delete(id);
        }
    }
}
