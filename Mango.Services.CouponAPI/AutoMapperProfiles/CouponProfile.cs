using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTOs;

namespace Mango.Services.CouponAPI.AutoMapperProfiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            // Map Coupon -> CouponDTO
            CreateMap<Coupon, CouponDTO>();

            // Map CouponDTO -> Coupon
            CreateMap<CouponDTO, Coupon>();
        }
    }
} 