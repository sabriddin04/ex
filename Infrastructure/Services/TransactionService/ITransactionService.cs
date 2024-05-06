using Domain.DTOs.TransactionDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TransactionService;

public interface ITransactionService
{
    Task<PagedResponse<List<GetTransactionsDTO>>> GetTransactionsAsync(TransactionFilter filter);
    Task<Response<GetTransactionsDTO>> GetTransactionByIdAsync(int transactionId);
    Task<Response<string>> CreateTransactionAsync(CreateTransactionDTO transaction);
    Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDTO transaction);
    Task<Response<bool>> RemoveTransactionAsync(int transactionId);
}
