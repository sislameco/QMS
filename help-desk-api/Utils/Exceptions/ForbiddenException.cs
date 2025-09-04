
namespace Utils.Exceptions;

public class ForbiddenException : Exception
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }
    public ForbiddenException(string message)
    {
        Message = message;
    }

    public ForbiddenException(ErrorDetail errors) 
    {
        ErrorDetail = errors;
    }
}

