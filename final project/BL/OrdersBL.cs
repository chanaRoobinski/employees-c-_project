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
    public class OrdersBL
    {
        public OrdersDTO convertOrdToDTO(OrdersTbl ord)
        {
            OrdersDTO dto = new OrdersDTO();
                        
            dto.OrdDate = ord.OrdDate;
            dto.OrdNum=ord.OrdNum;
            dto.Qty=ord.Qty;
            dto.CustId=ord.CustId;
            dto.ProdId=ord.ProdId;
            dto.CustName = ord.Cust != null ? ord.Cust.CustName : "";
            dto.ProdName = ord.Prod != null ? ord.Prod.ProdDesc : "";
            return dto;
        }

        public List<OrdersDTO> convertOrdListToDTO(List<OrdersTbl> list)
        {
            List<OrdersDTO> dto = new List<OrdersDTO>();
            foreach(OrdersTbl o in list)
            {
                dto.Add(convertOrdToDTO(o));
            }
            return dto;
        }

        public OrdersTbl covertToOrdersTbl(OrdersDTO o)
        {
            OrdersTbl ordersTbl = new OrdersTbl();
            ordersTbl.OrdDate = o.OrdDate;
            ordersTbl.OrdNum = o.OrdNum;
            ordersTbl.Qty = o.Qty;
            ordersTbl.CustId = o.CustId;
            ordersTbl.ProdId = o.ProdId;
            return ordersTbl;
        }

        OrdersDAL o = new OrdersDAL();  

        public async Task<List<OrdersDTO>> selectAllAsync()
        {
            return convertOrdListToDTO(await o.SelectAllAsync());
        }
        public async Task<OrdersDTO> selectById(int num)
        {
            return convertOrdToDTO((await o.SelectAllAsync()).FirstOrDefault());
        }

        public async Task<int> updateAsync(OrdersDTO ord)
        {
            return await o.updateAsync(covertToOrdersTbl(ord));
        }

        public Task<int> addAsync(OrdersDTO or)
        {
            return o.addAsync(covertToOrdersTbl(or));
        }

        public Task<int> delete(int id)
        {
            return o.deleteAsync(id);
        }
    }
}
