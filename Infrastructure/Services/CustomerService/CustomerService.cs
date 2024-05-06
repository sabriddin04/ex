using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.CustomerDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CustomerService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PagedResponse<List<GetCustomersDTO>>> GetCustomersAsync(CustomerFilter filter)
    {
        try
        {
            var customers = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                customers = customers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            var result = await customers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .OrderBy(x => x.Id)
                .ToListAsync();
            var total = await customers.CountAsync();

            var response = _mapper.Map<List<GetCustomersDTO>>(result);
            return new PagedResponse<List<GetCustomersDTO>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCustomersDTO>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetCustomersDTO>> GetCustomerByIdAsync(int CustomerId)
    {
        try
        {
            var existing = await _context.Customers.FirstOrDefaultAsync(x => x.Id == CustomerId);
            if (existing == null) return new Response<GetCustomersDTO>(HttpStatusCode.BadRequest, "Customer not found");
            var Customer = _mapper.Map<GetCustomersDTO>(existing);
            return new Response<GetCustomersDTO>(Customer);
        }
        catch (Exception e)
        {
            return new Response<GetCustomersDTO>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateCustomerAsync(CreateCustomerDTO customer)
    {
        try
        {
            var existing = await _context.Customers.AnyAsync(x => x.Name == customer.Name);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Customer already exists");
            var newCustomer = _mapper.Map<Customer>(customer);
            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDTO customer)
    {
        try
        {
            var existing = await _context.Customers.AnyAsync(x => x.Id == customer.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Customer not found");
            var newCustomer = _mapper.Map<Customer>(customer);
            _context.Customers.Update(newCustomer);
            await _context.SaveChangesAsync();
            return new Response<string>("Customer successfully updated");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<bool>> RemoveCustomerAsync(int CustomerId)
    {
        try
        {
            var existing = await _context.Customers.Where(x => x.Id == CustomerId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Customer not found")
                : new Response<bool>(true);
        }
        catch (DbException e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

