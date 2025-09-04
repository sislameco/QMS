namespace Utils.Exceptions
{
    public class ErrorDetail
    {
        public string? Message { get; set; }
        public string? Title { get; set; }
        public List<string> Errors { get; set; }= new List<string>();
        public ErrorDetail(string message)
        {
            Message = message;
        }
        public ErrorDetail(string message, List<string> errors)
        {
            Message = message;
            Errors = errors;
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public object Title { get; set; }
        public object Errors { get; set; }
    }
}
