namespace TradingAppMvc.Application.Models.Repository;

public class QueryCondition
{
   public QueryCondition(string column, string @operator, object value)
   {
      Column = column;
      Operator = @operator;
      Value = value;
   }
   public QueryCondition(string column, object value)
   {
      Column = column;
      Operator = "=";
      Value = value;
   }

   public string Column { get; private set; }
   public string Operator { get; private set; }
   public object Value { get; private set; }

}