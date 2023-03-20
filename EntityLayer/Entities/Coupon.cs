using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public int CouponCode { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        

    }
}
