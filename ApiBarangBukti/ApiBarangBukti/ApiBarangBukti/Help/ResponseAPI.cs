namespace ApiBarangBukti.Help
{
    public static class ResponseAPI<T>
    {
        public static GlobalObjectListResponse Create(string message, List<Object>? data)
        {
            return new()
            {
                Code = StatusCodes.Status200OK,
                Error = false,
                Message = message,
                Data = data
            };
        }

        public static GlobalObjectResponse<T> Create(string message, T data)
        {
            return new()
            {
                Code = StatusCodes.Status200OK,
                Error = false,
                Message = message,
                Data = data
            };
        }
    }

    public class ResponseAPI
    {
        public static GlobalResponse Create(string message)
        {
            return new()
            {
                Code = StatusCodes.Status200OK,
                Error = false,
                Message = message
            };
        }

        public static GlobalResponse CreateError(int code, string message)
        {
            return new()
            {
                Code = code,
                Error = true,
                Message = message
            };
        }
        public static GlobalErrorResponse CreateError(int code, string message, string traceId = null)
        {
            return new()
            {
                Code = code,
                Error = true,
                Message = message,
                TraceId = traceId
            };
        }
    }
}
