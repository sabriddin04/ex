using AutoMapper;
using Domain.DTOs.AccountDTO;
using Domain.DTOs.CustomerDTO;
using Domain.DTOs.TransactionDTO;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Account, GetAccountsDTO>().ReverseMap();
        CreateMap<Account, CreateAccountDTO>().ReverseMap();
        CreateMap<Account, UpdateAccountDTO>().ReverseMap();

        CreateMap<Customer, GetCustomersDTO>().ReverseMap();
        CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
        CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();

        CreateMap<Transaction, GetTransactionsDTO>().ReverseMap();
        CreateMap<Transaction, CreateTransactionDTO>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDTO>().ReverseMap();
    }
}
