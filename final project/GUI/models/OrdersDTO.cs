using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.models
{
    public class OrdersDTO
    {
        public int OrdNum { get; set; }
        public int? CustId { get; set; }
        public string CustName { get; set; }
        public int? ProdId { get; set; }
        public string ProdName { get; set; }
        public int? Qty { get; set; }
        public DateTime? OrdDate { get; set; }
    }
}
