using System.ComponentModel.DataAnnotations;

namespace SneakerSource.Services.CouponAPI.Controllers.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int DiscountAmount { get; set; }
        public int MinAmount { get; set; }
        public void PullProperties(Coupon toPullFrom)
        {
            Id = toPullFrom.Id;
            Code = toPullFrom.Code;
            DiscountAmount = toPullFrom.DiscountAmount;
            MinAmount = toPullFrom.MinAmount;
        }
    }
}
