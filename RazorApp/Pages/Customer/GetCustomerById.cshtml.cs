using Domain.DTOs.CustomerDTO;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer;

public class GetCustomerByIdModel : PageModel
{
    private readonly ICustomerService _CustomerService;

    public GetCustomerByIdModel(ICustomerService CustomerService)
    {
        _CustomerService = CustomerService;
    }

    public GetCustomersDTO Customer { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var customer = await _CustomerService.GetCustomerByIdAsync(id);
        Customer = customer.Data;
        if (Customer == null)
        {
            return NotFound();
        }

        return Page();
    }
}
