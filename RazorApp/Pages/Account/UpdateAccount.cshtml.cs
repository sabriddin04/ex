using Domain.DTOs.AccountDTO;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account;

public class UpdateAccountModel : PageModel
{
    private readonly IAccountService _accountService;

    public UpdateAccountModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [BindProperty]
    public UpdateAccountDTO Account { get; set; }

    public async Task<IActionResult> OnPostAsync(int id)
    {

        Account.Id = id;
        await _accountService.UpdateAccountAsync(Account);

        return RedirectToPage("/Account/GetAccounts");
    }
}
