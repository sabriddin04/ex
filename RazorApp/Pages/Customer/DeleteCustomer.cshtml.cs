using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer;

public class DeleteCustomerModel : PageModel
{
    private readonly ICustomerService _CustomerService;

    public DeleteCustomerModel(ICustomerService CustomerService)
    {
        _CustomerService = CustomerService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        await _CustomerService.RemoveCustomerAsync(Id);
        return RedirectToPage("/Customer/GetCustomers");
    }
}
