using Domain.Enums;

namespace Domain.DTOs.AccountDTO;

public class CreateAccountDTO
{
    public required string AccountNumber { get; set; } = " ";
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }
    public AccountType AccountType { get; set; }
}
