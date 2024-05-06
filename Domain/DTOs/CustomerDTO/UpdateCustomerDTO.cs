namespace Domain.DTOs.CustomerDTO;

public class UpdateCustomerDTO
{
    public int Id { get; set; }
    public required string Name { get; set; } = " ";
    public required string Email { get; set; } = " ";
    public required string Address { get; set; } = " ";
    public required string PhoneNumber { get; set; } = " ";
}

