using Domain.DTOs.TransactionDTO;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction;

public class GetTransactionByIdModel : PageModel
{
    private readonly ITransactionService _transactionService;

    public GetTransactionByIdModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public GetTransactionsDTO Transaction { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        Transaction = transaction.Data;
        if (Transaction == null)
        {
            return NotFound();
        }

        return Page();
    }
}

