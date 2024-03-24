namespace TradingAppMvc.Infraestructure.Repositories.Models;

public class QueryParameter
{
    public List<QueryCondition> Conditions { get; private set; } = new();
    public int? Limit { get; private set; }
    public int? Offset { get; private set; }
    public List<string> OrderByColumn = new List<string>();

    public QueryParameter AddCondition<T>(string column, string @operator, T value)
    {
        var queryParameter = new QueryCondition(column, @operator, value);
        Conditions.Add(queryParameter);
        return this;
    }

    public QueryParameter AddEqualCondition<T>(string column, T value)
    {
        var queryParameter = new QueryCondition(column, "=", value);
        Conditions.Add(queryParameter);
        return this;
    }

    public void AddOffset(int offset)
    {
        Offset = offset;
    }

    public void AddLimit(int limit)
    {
        Limit = limit;
    }

    public void AddOrderColumn(List<string> columns)
    {
        OrderByColumn.AddRange(columns);
    }
}