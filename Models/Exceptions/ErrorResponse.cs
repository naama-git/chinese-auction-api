namespace ChineseAuctionAPI.Models.Exceptions
{

    public class ErrorResponse : Exception
    {
        public int StatusCode { get; set; }
        public string Func { get; set; }
        public string Message { get; set; }
        public string DetailedMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public ErrorResponse(int statusCode, string func, string message, string detailedMessage)
        {
            StatusCode = statusCode;
            Func = func;
            Message = message;
            DetailedMessage = detailedMessage;
            Timestamp = DateTime.UtcNow;
        }
    }
}