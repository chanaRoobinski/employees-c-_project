using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class OrdersTbl
    {
        public int OrdNum { get; set; }
        public int? CustId { get; set; }
        public int? ProdId { get; set; }
        public int? Qty { get; set; }
        public DateTime? OrdDate { get; set; }

        public virtual CustomerTbl? Cust { get; set; }
        public virtual ProductsTbl? Prod { get; set; }

        internal OrdersTbl FirstOrDefualt()
        {
            throw new NotImplementedException();
        }
    }
}
