 #nullable disable

namespace CourseManagment.Domain.Result
{
    public class OperationResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public string ErrorCode { get; private set; }

        public bool IsFailure => !Success;

        protected OperationResult() { }

        private OperationResult(bool success, string message, string? errorCode = null)
        {
            Success = success;
            Message = message;
            ErrorCode = errorCode;
        }

        public static OperationResult Ok(string message = "Operación realizada correctamente.")
        {
            return new OperationResult(true, message);
        }

        public static OperationResult Fail(string message, string? errorCode = null)
        {
            return new OperationResult(false, message, errorCode);
        }
    }
}
