namespace PictureBase.Models
{
    public class ServiceResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public ServiceResponse()
        {
            Succeeded = true;
            Message = "Ok";
        }

        public ServiceResponse(string message, bool succeeded = false)
        {
            Succeeded = succeeded;
            Message = message;
        }
    }
}
