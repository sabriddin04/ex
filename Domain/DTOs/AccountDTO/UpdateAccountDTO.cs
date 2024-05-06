using Domain.Enums;

namespace Domain.DTOs.AccountDTO;

public class UpdateAccountDTO
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = " ";
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }
    public AccountType AccountType { get; set; }
}
