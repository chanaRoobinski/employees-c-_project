using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class ProductsTbl
    {
        public ProductsTbl()
        {
            OrdersTbls = new HashSet<OrdersTbl>();
        }

        public int ProdId { get; set; }
        public string? ProdDesc { get; set; }
        public double? Cost { get; set; }

        public virtual ICollection<OrdersTbl> OrdersTbls { get; set; }
    }
}
