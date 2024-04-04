using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    //private readonly BootcampContext _context;

    //public BankRepository(BootcampContext context)
    //{
    //    _context = context;
    //}

    //public async Task<BankDTO> Add(CreateBankModel model)
    //{
    //    var bankToCreate = new Banco
    //    {
    //        Nombre = model.Name,
    //        Direccion = model.Address,
    //        Mail = model.Mail,
    //        Telefono = model.Phone
    //    };

    //    _context.Bancos.Add(bankToCreate);

    //    await _context.SaveChangesAsync();

    //    var bankDTO = new BankDTO
    //    {
    //        Id = bankToCreate.Id,
    //        Name = bankToCreate.Nombre,
    //        Address = bankToCreate.Direccion,
    //        Mail = bankToCreate.Mail,
    //        Phone = bankToCreate.Telefono
    //    };

    //    return bankDTO;
    //}

    //public async Task<bool> Delete(int id)
    //{
    //    var bank = await _context.Bancos.FindAsync(id);

    //    if(bank is null) throw new Exception("Bank not found");

    //    _context.Bancos.Remove(bank);

    //    var result = await _context.SaveChangesAsync();

    //    return result > 0;
    //}

    //public async Task<List<BankDTO>> GetAll()
    //{
    //    var banks = await _context.Bancos.ToListAsync();

    //    var banksDTO = banks.Select(bank => new BankDTO
    //    {
    //        Id = bank.Id,
    //        Name = bank.Nombre,
    //        Address = bank.Direccion,
    //        Mail = bank.Mail,
    //        Phone = bank.Telefono
    //    }).ToList();

    //    return banksDTO;
    //}

    //public async Task<BankDTO> GetById(int id)
    //{
    //    var bank = await _context.Bancos.FindAsync(id);

    //    if(bank is null) throw new Exception("Bank not found");

    //    var bankDTO = new BankDTO
    //    {
    //        Id = bank.Id,
    //        Name = bank.Nombre,
    //        Address = bank.Direccion,
    //        Mail = bank.Mail,
    //        Phone = bank.Telefono
    //    };

    //    return bankDTO;
    //}

    //public async Task<bool> NameIsAlreadyTaken(string name)
    //{
    //    return await _context.Bancos.AnyAsync(bank => bank.Nombre == name);
    //}

    //public async Task<BankDTO> Update(UpdateBankModel model)
    //{
    //    var bank = await _context.Bancos.FindAsync(model.Id);

    //    if (bank is null) throw new Exception("Bank was not found");

    //    bank.Nombre = model.Name;
    //    bank.Direccion = model.Address;
    //    bank.Mail = model.Mail;
    //    bank.Telefono = model.Phone;

    //    _context.Bancos.Update(bank);

    //    await _context.SaveChangesAsync();

    //    var bankDTO = new BankDTO
    //    {
    //        Id = bank.Id,
    //        Name = bank.Nombre,
    //        Address = bank.Direccion,
    //        Mail = bank.Mail,
    //        Phone = bank.Telefono
    //    };

    //    return bankDTO;
    //}
}
