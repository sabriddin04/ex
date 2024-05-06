namespace Domain.DTOs.TransactionDTO;

public class CreateTransactionDTO
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
}
