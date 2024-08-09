namespace TradingAppMvc.Application.Models.Repository;

public class QueryParameter
{
   public List<QueryCondition> Conditions { get; private set; } = [];
   public int? Limit { get; private set; }
   public int? Offset { get; private set; }
   public List<string> OrderByColumn = new List<string>();

   public QueryParameter() { }
   public QueryParameter(List<QueryCondition> whereConditions)
   {
      Conditions = whereConditions;
   }
   public QueryParameter WhereCondition<T>(string column, string @operator, T value)
   {
      var queryParameter = new QueryCondition(column, @operator, value);
      Conditions.Add(queryParameter);
      return this;
   }

   public QueryParameter EqualCondition<T>(string column, T value)
   {
      var queryParameter = new QueryCondition(column, "=", value);
      Conditions.Add(queryParameter);
      return this;
   }

   public QueryParameter AddOffset(int offset)
   {
      Offset = offset;
      return this;
   }

   public void AddLimit(int limit)
   {
      Limit = limit;
   }

   public QueryParameter AddOrderColumn(List<string> columns)
   {
      OrderByColumn.AddRange(columns);
      return this;
   }
}