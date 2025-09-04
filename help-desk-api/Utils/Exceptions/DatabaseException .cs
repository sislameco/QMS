using System;
using System.Net;

namespace Utils.Exceptions;

public class DatabaseException : Exception
{
    public ErrorDetail? ErrorDetail { get; }
    public string Message { get; }

    public DatabaseException(string message)
    {
        Message = message;
    }

    public DatabaseException(ErrorDetail errors)
    {
        ErrorDetail = errors;
    }
}
