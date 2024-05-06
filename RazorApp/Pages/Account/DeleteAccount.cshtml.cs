using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account;

public class DeleteAccountModel : PageModel
{
    private readonly IAccountService _AccountService;

    public DeleteAccountModel(IAccountService accountService)
    {
        _AccountService = accountService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        await _AccountService.RemoveAccountAsync(Id);
        return RedirectToPage("/Account/GetAccounts");
    }
}

