namespace Utils.Exceptions;

public class UnAuthorizedException : Exception
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }

    public UnAuthorizedException(string message)
    {
        Message = message;
    }

    public UnAuthorizedException(ErrorDetail errors)
    {
        ErrorDetail = errors;
    }
}

