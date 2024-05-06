using Domain.Enums;

namespace Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }
    public AccountType AccountType { get; set; }

    public Customer? Customer { get; set; }
    public List<Transaction>? Transactions { get; set; }

}