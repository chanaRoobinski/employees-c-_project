using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.models;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class ProductsBL
    {
        public ProductsDTO convertProdToDTO(ProductsTbl p)
        {
            ProductsDTO productsDTO = new ProductsDTO();
            productsDTO.ProdId = p.ProdId;
            productsDTO.ProdDesc=p.ProdDesc;
            productsDTO.Cost=p.Cost;
            return productsDTO;
        }

        public List<ProductsDTO> convertProdListToDTO(List<ProductsTbl> list)
        {
            List<ProductsDTO> productsDTO = new List<ProductsDTO>();    
            foreach(ProductsTbl p in list)
            {
                productsDTO.Add(convertProdToDTO(p));
            }
            return productsDTO;
        }

        public ProductsTbl convertToProductTbl(ProductsDTO p)
        {
            ProductsTbl prod=new ProductsTbl();
            prod.ProdId = p.ProdId;
            prod.ProdDesc = p.ProdDesc;
            prod.Cost = p.Cost;
            return prod;
        }

        ProductsDAL p=new ProductsDAL();

        public async Task<List<ProductsDTO>> selectAllAsync()
        {
            return convertProdListToDTO(await p.selectAllAsync());
        }

        public async Task<ProductsDTO> selectById(int id)
        {
            return convertProdToDTO((await p.selectAllAsync()).FirstOrDefault());
        }

        public async Task<int> updateAsync(ProductsDTO prod)
        {
            return await p.updateAsync(convertToProductTbl(prod));
        }

        public Task<int> addAsync(ProductsDTO pr)
        {
            return p.addAsync(convertToProductTbl(pr));
        }

        public Task<int> delete(int id)
        {
            return p.deleteAsync(id);
        }
    }
}
