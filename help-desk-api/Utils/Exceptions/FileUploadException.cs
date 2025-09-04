namespace Utils.Exceptions
{
    public class FileUploadException:Exception
    {
        public ErrorDetail? ErrorDetail { get; }
        public string Message { get; }

        public FileUploadException(string message)
        {
            Message = message;
        }

        public FileUploadException(ErrorDetail errors)
        {
            ErrorDetail = errors;
        }
    }
}
