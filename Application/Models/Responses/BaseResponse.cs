namespace TradingAppMvc.Application.Models.Responses
{
    public class BaseResponse<T> where T : class
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Result { get; private set; }
        public void SetSuccess()
        {
            Success = true;
        }
        public void SetResult(T result)
        {
            Result = result;
        }
        public void SetError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}