namespace Models.Dto.GlobalDto
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ListResponse<T>: BaseResponse
    {
        public List<T> Data { get; set; } = new List<T>();
    }

    public class ObjectResponse<T>: BaseResponse
    {
        public T Data { get; set; }
    }

    public class PaginatedResponse<T>: BaseResponse
    {
        public required List<T> Data { get; set; }
        public int Count { get; set; }
    }
}
