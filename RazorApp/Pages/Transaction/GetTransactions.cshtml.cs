using System.Net;
using Domain.DTOs.TransactionDTO;
using Domain.Filters;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction;

[IgnoreAntiforgeryToken]
public class GetTransactionsModel : PageModel
{
    private readonly ITransactionService _transactionService;

    public GetTransactionsModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [BindProperty(SupportsGet = true)]
    public TransactionFilter Filter { get; set; }

    public List<GetTransactionsDTO> Transactions { get; set; }
    public int TotalPages { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var response = await _transactionService.GetTransactionsAsync(Filter);
            Transactions = response.Data;
            TotalPages = response.TotalPages;
            return Page();
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
