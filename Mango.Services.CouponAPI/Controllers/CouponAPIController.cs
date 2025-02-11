using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        // add dependency injection for dbcontext
        private readonly AppDbContext _context;
        protected ResponseDTO _response;
        private readonly IMapper _mapper;

        public CouponAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new ResponseDTO();
            _mapper = mapper;
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

        // Route to get a coupon by code
        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<ResponseDTO>> GetCouponByCode(string code)
        {
            try
            {
                Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower() == code.ToLower());
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
        //  create coupon
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateCoupon(CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                await _context.Coupons.AddAsync(coupon);
                await _context.SaveChangesAsync();
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.Message;
                _response.DisplayMessage = "Error creating coupon";
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }
        
        // Update coupon
        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateCoupon(CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                _context.Coupons.Update(coupon);
                await _context.SaveChangesAsync();
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.Message;
                _response.DisplayMessage = "Error updating coupon";
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }

        // Delete coupon
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO>> DeleteCoupon(int id)
        {
            try
            {
                Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
                if (coupon == null)
                {
                    _response.DisplayMessage = "Coupon not found";
                    return NotFound(_response);
                }
                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();
                _response.Result = _mapper.Map<CouponDTO>(coupon);
                _response.DisplayMessage = "Coupon deleted successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.Message;
                _response.DisplayMessage = "Error deleting coupon";
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }
    }
}
