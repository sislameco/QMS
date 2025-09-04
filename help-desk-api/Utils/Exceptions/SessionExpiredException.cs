namespace Utils.Exceptions;

public class SessionExpiredException : Exception
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }

    public SessionExpiredException(string message) {
        Message = message;
    }
    public SessionExpiredException(ErrorDetail errors)
    {
        ErrorDetail = errors;
    }
}
