using Domain.DTOs.AccountDTO;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account;
[IgnoreAntiforgeryToken]
public class CreateAccountModel : PageModel
{
    private readonly IAccountService _accountService;

    public CreateAccountModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [BindProperty] public CreateAccountDTO AccountDto { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _accountService.CreateAccountAsync(AccountDto);

        return RedirectToPage("/Account/GetAccounts");
    }
}
