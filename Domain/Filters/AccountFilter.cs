namespace Domain.Filters;

public class AccountFilter : PaginationFilter
{
    public decimal? Balance { get; set; }
   
}
