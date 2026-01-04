namespace ChineseAuctionAPI.Models.Exceptions
{

    public class ErrorResponse : Exception
    {
        public int StatusCode { get; set; }
        public string Func { get; set; }
        public string Message { get; set; }
        public string DetailedMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Method { get; set; }

        public string? Location { get; set; } // repo, srv, cont, mid ...

        public ErrorResponse(int statusCode, string func, string message, string detailedMessage, DateTime timestamp, string? method, string? location)
        {
            StatusCode = statusCode;
            Func = func;
            Message = message;
            DetailedMessage = detailedMessage;
            Timestamp = DateTime.UtcNow;
            Method = method;
            Location = location;
        }
    }
}