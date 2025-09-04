namespace Utils.Exceptions;

public class RequestTimeoutException : TimeoutException
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }

    public RequestTimeoutException(string message) {
        Message = message;
    }
    public RequestTimeoutException(ErrorDetail errors)
    {
        ErrorDetail = errors;
    }
}
