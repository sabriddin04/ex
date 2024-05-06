namespace Domain.Filters;

public class TransactionFilter : PaginationFilter
{
    public decimal? Amount { get; set; }

}
