namespace SneakerSource.Services.CouponAPI.Controllers.Models.Transfer
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = "";
        public void Sucessful(object result)
        {
            Result = result;
            IsSuccessful = true;
        }
        public void Sucessful()
        {
            IsSuccessful = true;
        }
        public void Unsuccessful(string message)
        {
            IsSuccessful = false;
            Message = message;
        }
        public void Unsuccessful()
        {
            IsSuccessful = false;
        }
    }
}
