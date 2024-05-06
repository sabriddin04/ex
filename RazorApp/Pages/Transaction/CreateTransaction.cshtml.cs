using Domain.DTOs.TransactionDTO;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction
{
    [IgnoreAntiforgeryToken]
    public class CreateTransactionModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public CreateTransactionModel(ITransactionService TransactionService)
        {
            _transactionService = TransactionService;
        }

        [BindProperty]
        public CreateTransactionDTO TransactionDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _transactionService.CreateTransactionAsync(TransactionDto);
            return RedirectToPage("/Transaction/GetTransactions");
        }
    }
}
