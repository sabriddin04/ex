using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }

    public Account? FromAccount { get; set; }

}
