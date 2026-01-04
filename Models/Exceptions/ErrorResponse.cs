namespace ChineseAuctionAPI.Models.Exceptions
{
    public class Exceptions
    {

    }
}
public class ErrorResponse:Exception
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string DetailedMessage { get; set; } 
    public int ErrorId { get; set; }
}