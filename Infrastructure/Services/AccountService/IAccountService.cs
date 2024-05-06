using Domain.DTOs.AccountDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.AccountService;

public interface IAccountService
{
    Task<PagedResponse<List<GetAccountsDTO>>> GetAccountsAsync(AccountFilter filter);
    Task<Response<GetAccountsDTO>> GetAccountByIdAsync(int accountId);
    Task<Response<string>> CreateAccountAsync(CreateAccountDTO account);
    Task<Response<string>> UpdateAccountAsync(UpdateAccountDTO account);
    Task<Response<bool>> RemoveAccountAsync(int accountId);
}
