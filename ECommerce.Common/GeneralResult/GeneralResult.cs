using System.Text.Json.Serialization;

namespace ECommerce.Common
{
    public class GeneralResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, List<Errors>>? Errors { get; set; }

        public static GeneralResult SuccessResult(string message = "Success")
        {
            return new GeneralResult { Success = true, Message = message, Errors = null };
        }

        public static GeneralResult NotFound(string message = "Resource not found")
        {
            return new GeneralResult { Success = false, Message = message, Errors = null };
        }

        public static GeneralResult FailureResult (string message = "Operation failed")
        {
            return new GeneralResult { Success = false, Message = message, Errors = null };
        }

        public static GeneralResult FailureResult (Dictionary<string, List<Errors>> errors, string message = "Validation failed")
        {
            return new GeneralResult { Success = false, Message = message, Errors = errors };
        }
    }
    public class GeneralResult<T> : GeneralResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public static GeneralResult<T> SuccessResult(T data, string message = "Success")
        {
            return new GeneralResult<T> { Success = true, Message = message, Data = data, Errors = null };
        }
        public static new GeneralResult<T> SuccessResult(string message = "Success")
        {
            return new GeneralResult<T> { Success = true, Message = message, Data = default, Errors = null };
        }

        public static new GeneralResult<T> NotFound(string message = "Resource not found")
        {
            return new GeneralResult<T> { Success = false, Message = message, Data = default, Errors = null };
        }

        public static new GeneralResult<T> FailureResult(string message = "Operation failed")
        {
            return new GeneralResult<T> { Success = false, Message = message, Data = default, Errors = null };
        }

        public static new GeneralResult<T> FailureResult(Dictionary<string, List<Errors>> errors, string message = "Validation failed")
        {
            return new GeneralResult<T> { Success = false, Message = message, Data = default, Errors = errors };
        }
    }
}
