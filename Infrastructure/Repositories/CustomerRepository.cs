using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BootcampContext _context;

    public CustomerRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<CustomerDTO> Add(CreateCustomerModel model)
    {
        var bank = await _context.Banks.FindAsync(model.BankId);
        var customerToCreate = new Customer
        {
            Name = model.Name,
            Lastname = model.Lastname,
            DocumentNumber = model.DocumentNumber,
            Address = model.Address,
            Mail = model.Mail,
            Phone = model.Phone,
            CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model.CustomerStatus),
            BankId = model.BankId,
            Birth =model.Birth
        };

        _context.Customers.Add(customerToCreate);
        await _context.SaveChangesAsync();

        var customerDTO = new CustomerDTO
        {
            Id=customerToCreate.Id,
            Name = customerToCreate.Name,
            Lastname = customerToCreate.Lastname,
            DocumentNumber = customerToCreate.DocumentNumber,
            Address = customerToCreate.Address,
            Mail = customerToCreate.Mail,
            Phone = customerToCreate.Phone,
            CustomerStatus = nameof(customerToCreate.CustomerStatus),
            Birth = customerToCreate.Birth,
            Bank = new BankDTO
            {
                Id = customerToCreate.Bank.Id,
                Name = customerToCreate.Bank.Name,
                Phone = customerToCreate.Bank.Phone,
                Mail = customerToCreate.Bank.Mail,
                Address = customerToCreate.Bank.Address
            }
        };
        return customerDTO;
    }

    public async Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter)
    {
            var query = _context.Customers
                .Include(c => c.Bank)
                .AsQueryable();

        if (filter.FullName is not null)
        {
            query = query.Where(x =>
                x.Name != null &&
                (x.Name + " " + x.Lastname).Equals(filter.FullName) ||
                (x.Name != null && x.Name.Equals(filter.FullName)) ||
                (x.Lastname != null && x.Lastname.Equals(filter.FullName)));
        }

        if (filter.BirthYearFrom is not null)
        {
            query = query.Where(x =>
                x.Birth != null &&
                x.Birth.Value.Year >= filter.BirthYearFrom);
        }
        if (filter.BankId is not null)
        {
            query = query.Where(x =>
                x.Bank != null &&
                x.BankId==filter.BankId);
        }

        if (filter.DocumentNumber is not null)
        {
            query = query.Where(x =>
                x.DocumentNumber != null &&
                x.DocumentNumber.Equals(filter.DocumentNumber));
        }
        if (filter.Mail is not null)
        {
            query = query.Where(x =>
                x.Mail != null &&
                x.Mail.Equals(filter.Mail));
        }


        if (filter.BirthYearTo is not null)
        {
            query = query.Where(x =>
                x.Birth != null &&
                x.Birth.Value.Year <= filter.BirthYearTo);
        }


        var result = await query.ToListAsync();

        return result.Select(x => new CustomerDTO
        {
            Id = x.Id,
            Name = x.Name,
            Lastname = x.Lastname,
            DocumentNumber = x.DocumentNumber,
            Address = x.Address,
            Mail = x.Mail,
            Phone = x.Phone,
            CustomerStatus = nameof(x.CustomerStatus),
            Birth = x.Birth,
            Bank = new BankDTO
            {
                Id = x.Bank.Id,
                Name = x.Bank.Name,
                Phone = x.Bank.Phone,
                Mail = x.Bank.Mail,
                Address = x.Bank.Address
            }
        }).ToList();
    }

    public async Task<CustomerDTO> Update(UpdateCustomerModel model)
    {
        var bank = await _context.Banks.FindAsync(model.BankId);
        var customerToUpdate = new Customer
        {
            Name = model.Name,
            Lastname = model.Lastname,
            DocumentNumber = model.DocumentNumber,
            Address = model.Address,
            Mail = model.Mail,
            Phone = model.Phone,
            CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model.CustomerStatus),
            BankId = model.BankId,
            Birth = model.Birth
        };

        _context.Customers.Add(customerToUpdate);
        await _context.SaveChangesAsync();

        var customerDTO = new CustomerDTO
        {
            Id = customerToUpdate.Id,
            Name = customerToUpdate.Name,
            Lastname = customerToUpdate.Lastname,
            DocumentNumber = customerToUpdate.DocumentNumber,
            Address = customerToUpdate.Address,
            Mail = customerToUpdate.Mail,
            Phone = customerToUpdate.Phone,
            CustomerStatus = nameof(customerToUpdate.CustomerStatus),
            Birth = customerToUpdate.Birth,
            Bank = new BankDTO
            {
                Id = customerToUpdate.Bank.Id,
                Name = customerToUpdate.Bank.Name,
                Phone = customerToUpdate.Bank.Phone,
                Mail = customerToUpdate.Bank.Mail,
                Address = customerToUpdate.Bank.Address
            }
        };
        return customerDTO;
    }
}