namespace TradingAppMvc.Infraestructure.Repositories.Models;

public class QueryCondition
{
    public QueryCondition(string column, string @operator, object value)
    {
        Column = column;
        Operator = @operator;
        Value = value;
    }

    public string Column { get; private set; }
    public string Operator { get; private set; }
    public object Value { get; private set; }

}