using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderService.Data
{
    public class Result<T> where T : class
    {
        public Result(T? value, bool isSuccess, List<string>? errors)
        {
            Value = value;
            IsSuccess = isSuccess;
            Errors = errors ?? new List<string>();
        }
        public T? Value { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public List<string> Errors { get; private set; }

        public static Result<T> OkOnlyMessage(string message) => new(null, true, new List<string> { message });
        public static Result<T> Ok(T value) => new(value, true, null);
        public static Result<T> Error(string error) => new(default, false, new List<string> { error });
        public static Result<T> ErrorList(List<string> errors) => new(default, false, errors);
    }
}
