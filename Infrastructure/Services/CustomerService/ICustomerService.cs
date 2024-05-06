using Domain.DTOs.CustomerDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CustomerService;

public interface ICustomerService
{
    Task<PagedResponse<List<GetCustomersDTO>>> GetCustomersAsync(CustomerFilter filter);
    Task<Response<GetCustomersDTO>> GetCustomerByIdAsync(int customerId);
    Task<Response<string>> CreateCustomerAsync(CreateCustomerDTO customer);
    Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDTO customer);
    Task<Response<bool>> RemoveCustomerAsync(int customerId);
}
