namespace TradingAppMvc.Models.Responses
{
   public class ApplicationResponse<T> where T : class
   {
      public ApplicationResponse()
      {
         Data = null;
         Success = true;
      }
      public ApplicationResponse(T result)
      {
         Data = result;
         Success = true;
      }
      public bool Success { get; private set; }
      public string ErrorMessage { get; private set; } = string.Empty;
      public T? Data { get; private set; }
      public void SetResult(T result)
      {
         Data = result;
      }
      public void SetError(string errorMessage)
      {
         Success = false;
         ErrorMessage = errorMessage;
      }
   }
}
