using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.AccountDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public AccountService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PagedResponse<List<GetAccountsDTO>>> GetAccountsAsync(AccountFilter filter)
    {
        try
        {
            var accounts = _context.Accounts.AsQueryable();
            if (filter.Balance != null)
                accounts = accounts.Where(x => x.Balance == filter.Balance);
            var result = await accounts.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .OrderBy(x => x.Id)
                .ToListAsync();
            var total = await accounts.CountAsync();

            var response = _mapper.Map<List<GetAccountsDTO>>(result);
            return new PagedResponse<List<GetAccountsDTO>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetAccountsDTO>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetAccountsDTO>> GetAccountByIdAsync(int accountId)
    {
        try
        {
            var existing = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (existing == null) return new Response<GetAccountsDTO>(HttpStatusCode.BadRequest, "Account not found");
            var Account = _mapper.Map<GetAccountsDTO>(existing);
            return new Response<GetAccountsDTO>(Account);
        }
        catch (Exception e)
        {
            return new Response<GetAccountsDTO>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateAccountAsync(CreateAccountDTO account)
    {
        try
        {
            var existing = await _context.Accounts.AnyAsync(x => x.AccountNumber == account.AccountNumber);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Account already exists");
            var newAccount = _mapper.Map<Account>(account);
            await _context.Accounts.AddAsync(newAccount);
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


    public async Task<Response<string>> UpdateAccountAsync(UpdateAccountDTO account)
    {
        try
        {
            var existing = await _context.Accounts.AnyAsync(x => x.Id == account.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Account not found");
            var newAccount = _mapper.Map<Account>(account);
            _context.Accounts.Update(newAccount);
            await _context.SaveChangesAsync();
            return new Response<string>("Account successfully updated");
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


    public async Task<Response<bool>> RemoveAccountAsync(int accountId)
    {
        try
        {
            var existing = await _context.Accounts.Where(x => x.Id == accountId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Account not found")
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
