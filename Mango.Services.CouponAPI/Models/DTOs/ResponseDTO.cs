namespace Mango.Services.CouponAPI.Models.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public string ErrorMessages { get; set; } = "";
    }
}
