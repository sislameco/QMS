
namespace Utils.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }

    public ResourceNotFoundException(string message)
    {
        Message = message;
    }

    public ResourceNotFoundException(ErrorDetail errors)
    {
        ErrorDetail = errors;
    }
}
