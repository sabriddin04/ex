using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.TransactionDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public TransactionService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PagedResponse<List<GetTransactionsDTO>>> GetTransactionsAsync(TransactionFilter filter)
    {
        try
        {
            var transactions = _context.Transactions.AsQueryable();
            if (filter.Amount != null)
                transactions = transactions.Where(x => x.Amount == filter.Amount);
            var result = await transactions.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .OrderBy(x => x.Id)
                .ToListAsync();
            var total = await transactions.CountAsync();

            var response = _mapper.Map<List<GetTransactionsDTO>>(result);
            return new PagedResponse<List<GetTransactionsDTO>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTransactionsDTO>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetTransactionsDTO>> GetTransactionByIdAsync(int transactionId)
    {
        try
        {
            var existing = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == transactionId);
            if (existing == null) return new Response<GetTransactionsDTO>(HttpStatusCode.BadRequest, "Transaction not found");
            var Transaction = _mapper.Map<GetTransactionsDTO>(existing);
            return new Response<GetTransactionsDTO>(Transaction);
        }
        catch (Exception e)
        {
            return new Response<GetTransactionsDTO>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateTransactionAsync(CreateTransactionDTO transaction)
    {
        try
        {
            var newTransaction = _mapper.Map<Transaction>(transaction);

            var fromAccount = _context.Accounts.FirstOrDefault(x => x.Id == newTransaction.FromAccountId);
            if (fromAccount == null) return new Response<string>("Sender Account not found");
            if (newTransaction.Amount > fromAccount.Balance) return new Response<string>("Sender Account have not this money");
            fromAccount.Balance -= newTransaction.Amount;

            var toAccount = _context.Accounts.FirstOrDefault(x => x.Id == newTransaction.ToAccountId);
            if (fromAccount == null) return new Response<string>("Account who accept money not found");
            toAccount.Balance += newTransaction.Amount;

            await _context.Transactions.AddAsync(newTransaction);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDTO transaction)
    {
        try
        {
            var existing = await _context.Transactions.AnyAsync(x => x.Id == transaction.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Transaction not found");
            var newTransaction = _mapper.Map<Transaction>(transaction);
            _context.Transactions.Update(newTransaction);
            await _context.SaveChangesAsync();
            return new Response<string>("Transaction successfully updated");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<bool>> RemoveTransactionAsync(int transactionId)
    {
        try
        {
            var existing = await _context.Transactions.Where(x => x.Id == transactionId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Transaction not found")
                : new Response<bool>(true);
        }
        catch (DbException e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
