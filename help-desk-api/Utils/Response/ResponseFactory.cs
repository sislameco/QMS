using Models.Dto.GlobalDto;

namespace Utils.Response
{
    public static class ResponseFactory
    {
        public static ListResponse<T> SuccessList<T>(List<T> data, string? message = null) =>
            new() { Data = data, Message = message, Success = true };

        public static ObjectResponse<T> Success<T>(T data, string? message = null) =>
            new() { Data = data, Message = message, Success = true };

        public static PaginatedResponse<T> Paginated<T>(List<T> data, int count, string? message = null) =>
            new() { Data = data, Count = count, Message = message, Success = true };
    }

}
