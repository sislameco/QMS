namespace Utils.Exceptions;

public class BadRequestException : Exception
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }
    public BadRequestException(string message) {
        Message = message;
    }
    public BadRequestException(ErrorDetail errors)
    {
        ErrorDetail = errors;
    }
}
