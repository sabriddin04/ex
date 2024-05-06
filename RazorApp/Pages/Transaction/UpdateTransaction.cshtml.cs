using Domain.DTOs.TransactionDTO;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction;

public class UpdateTransactionModel : PageModel
{
    private readonly ITransactionService _transactionService;

    public UpdateTransactionModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [BindProperty]
    public UpdateTransactionDTO Transaction { get; set; }

    public async Task<IActionResult> OnPostAsync(int id)
    {

        Transaction.Id = id;
        await _transactionService.UpdateTransactionAsync(Transaction);

        return RedirectToPage("/Transaction/GetTransactions");
    }
}
