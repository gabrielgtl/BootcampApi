

using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Currency;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly BootcampContext _context;
    public CurrencyRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<CurrencyDTO> Add(CreateCurrencyModel model)
    {
        var currencyToCreate = model.Adapt<Currency>();

        _context.Currencies.Add(currencyToCreate);

        await _context.SaveChangesAsync();

        var currencyDTO = currencyToCreate.Adapt<CurrencyDTO>();

        return currencyDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var currency = await _context.Currencies.FindAsync(id);

        if (currency is null) throw new Exception("Currency not found");

        _context.Currencies.Remove(currency);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<CurrencyDTO> GetById(int id)
    {
        var currency = await _context.Currencies.FindAsync(id);

        if (currency is null) throw new NotFoundException($"Currency with id: {id} doest not exist");

        var currencyDTO = currency.Adapt<CurrencyDTO>();

        return currencyDTO;
    }

    public async Task<List<CurrencyDTO>> GetByName(FilterCurrencyModel filter)
    {
        var query = _context.Currencies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            var nameUpper = filter.Name.ToUpper();
            query = query.Where(x =>
                x.Name != null &&
                x.Name.ToUpper() == nameUpper);
        }

        var result = await query.ToListAsync();
        var currencyDTOs = result.Adapt<List<CurrencyDTO>>();
        return currencyDTOs;
    }


    public async Task<CurrencyDTO> Update(UpdateCurrencyModel model)
    {
        var currency = await _context.Currencies.FindAsync(model.Id);

        if (currency is null) throw new Exception("Currency was not found");

        model.Adapt(currency);

        _context.Currencies.Update(currency);

        await _context.SaveChangesAsync();

        var currencyDTO = currency.Adapt<CurrencyDTO>();

        return currencyDTO;
    }
}
