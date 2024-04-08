using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Infrastructure.Migrations;
using Mapster;
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
        var customerToCreate = model.Adapt<Customer>();

        _context.Customers.Add(customerToCreate);
        await _context.SaveChangesAsync();

        var customerDTO = customerToCreate.Adapt<CustomerDTO>();

        return customerDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer is null) throw new Exception("Customer not found");

        _context.Customers.Remove(customer);

        var result = await _context.SaveChangesAsync();

        return result > 0;
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
        var customerDTOs = result.Adapt<List<CustomerDTO>>();
        return customerDTOs;
    }
    public async Task<List<CustomerDTO>> GetAll()
    {
        var query = _context.Customers
            .Include(c => c.Bank)
            .AsQueryable();

        var result = await query.ToListAsync();
        var customerDTO = result.Adapt<List<CustomerDTO>>();

        return customerDTO;
    }
    public async Task<CustomerDTO> GetById(int id)
    {
        var query = _context.Customers
            .Include(c => c.Bank)
            .AsQueryable();

        var customer = await query.FirstOrDefaultAsync(c => c.Id == id);

        if (customer is null)
            throw new Exception("Customer not found");

        var customerDTO = customer.Adapt<CustomerDTO>();

        return customerDTO;
    }
    public async Task<CustomerDTO> Update(UpdateCustomerModel model)
    {
        var bank = await _context.Banks.FindAsync(model.BankId);
        var customer = await _context.Customers.FindAsync(model.Id);
        if (customer is null) throw new Exception("Customer was not found");
        model.Adapt(customer);
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        var customerDTO = customer.Adapt<CustomerDTO>();
        return customerDTO;
    }
}