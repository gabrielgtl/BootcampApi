using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    private readonly BootcampContext _context;

    public BankRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<BankDTO> Add(CreateBankModel model)
    {
        var bankToCreate = new Bank
        {
            Name = model.Name,
            Address = model.Address,
            Mail = model.Mail,
            Phone = model.Phone
        };

        _context.Banks.Add(bankToCreate);

        await _context.SaveChangesAsync();

        var bankDTO = new BankDTO
        {
            Id = bankToCreate.Id,
            Name = bankToCreate.Name,
            Address = bankToCreate.Address,
            Mail = bankToCreate.Mail,
            Phone = bankToCreate.Phone
        };

        return bankDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var bank = await _context.Banks.FindAsync(id);

        if (bank is null) throw new Exception("Bank not found");

        _context.Banks.Remove(bank);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<BankDTO>> GetAll()
    {
        var banks = await _context.Banks.ToListAsync();

        var banksDTO = banks.Select(bank => new BankDTO
        {
            Id = bank.Id,
            Name = bank.Name,
            Address = bank.Address,
            Mail = bank.Mail,
            Phone = bank.Phone
        }).ToList();

        return banksDTO;
    }

    public async Task<BankDTO> GetById(int id)
    {
        var bank = await _context.Banks.FindAsync(id);

        if (bank is null) throw new Exception("Bank not found");

        var bankDTO = new BankDTO
        {
            Id = bank.Id,
            Name = bank.Name,
            Address = bank.Address,
            Mail = bank.Mail,
            Phone = bank.Phone
        };

        return bankDTO;
    }

    public async Task<bool> NameIsAlreadyTaken(string name)
    {
        return await _context.Banks.AnyAsync(bank => bank.Name == name);
    }

    public async Task<BankDTO> Update(UpdateBankModel model)
    {
        var bank = await _context.Banks.FindAsync(model.Id);

        if (bank is null) throw new Exception("Bank was not found");

        bank.Name = model.Name;
        bank.Address = model.Address;
        bank.Mail = model.Mail;
        bank.Phone = model.Phone;

        _context.Banks.Update(bank);

        await _context.SaveChangesAsync();

        var bankDTO = new BankDTO
        {
            Id = bank.Id,
            Name = bank.Name,
            Address = bank.Address,
            Mail = bank.Mail,
            Phone = bank.Phone
        };

        return bankDTO;
    }
}