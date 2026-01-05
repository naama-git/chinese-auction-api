namespace ChineseAuctionAPI.Models.Exceptions
{

    public class ErrorResponse : Exception
    {
        public int StatusCode { get; set; }
        public string Func { get; set; }
        public string DetailedMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Method { get; set; }

        public string? Location { get; set; } // repo, srv, cont, mid ...

        public ErrorResponse(int statusCode, string func, string message, string detailedMessage, string? method=null, string? location=null):base(message)
        {
            StatusCode = statusCode;
            Func = func;
            DetailedMessage = detailedMessage;
            Timestamp = DateTime.UtcNow;
            Method = method;
            Location = location;
        }
    }
}