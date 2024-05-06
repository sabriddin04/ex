using Domain.DTOs.CustomerDTO;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer;

public class UpdateCustomerModel : PageModel
{
    private readonly ICustomerService _CustomerService;

    public UpdateCustomerModel(ICustomerService CustomerService)
    {
        _CustomerService = CustomerService;
    }

    [BindProperty]
    public UpdateCustomerDTO Customer { get; set; }

    public async Task<IActionResult> OnPostAsync(int id)
    {

        Customer.Id = id;
        await _CustomerService.UpdateCustomerAsync(Customer);

        return RedirectToPage("/Customer/GetCustomers");
    }
}
