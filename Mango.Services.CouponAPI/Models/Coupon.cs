﻿namespace Mango.Services.CouponAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }
    }
}
