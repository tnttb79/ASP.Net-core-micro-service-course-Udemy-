using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        // add dependency injection for dbcontext
        private readonly AppDbContext _context;
        protected ResponseDTO _response;

        public CouponAPIController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }

        // Route to get all coupons
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetCoupons()
        {
            try
            {
                IEnumerable<Coupon> coupons = await _context.Coupons.ToListAsync();
                _response.Result = coupons;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.Message;
                _response.DisplayMessage = "Error retrieving coupons";
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }

        // Route to get a coupon by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetCoupon(int id)
        {
            try
            {
                Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
                if (coupon == null)
                {
                    _response.DisplayMessage = "Coupon not found";
                    return NotFound(_response);
                }
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.Message;
                _response.DisplayMessage = "Error retrieving coupon";
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }
    }
}
