using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Promotion;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PromotionRepository : IPromotionRepository
{
    private readonly BootcampContext _context;

    public PromotionRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<PromotionDTO> Add(CreatePromotionModel model)
    {

        var promotion = model.Adapt<Promotion>();

        foreach (int enterpriseId in model.Enterprises)
        {
            var promotionEnterprise = new PromotionEnterprise
            {
                Promotion = promotion,
                EnterpriseId = enterpriseId
            };
            _context.PromotionEnterprises.Add(promotionEnterprise);
        }

        _context.Promotions.Add(promotion);

        await _context.SaveChangesAsync();

        var createdPromotion = await _context.Promotions
            .Include(a => a.PromotionsEnterprises)
            .ThenInclude(a => a.Enterprise)
            .FirstOrDefaultAsync(a => a.Id == promotion.Id);


        var promotionDTO = promotion.Adapt<PromotionDTO>();

        return promotionDTO;
    }

        public async Task<bool> Delete(int id)
    {
        var promotion = await _context.Promotions.FindAsync(id);
        if (promotion == null) { throw new NotFoundException($"Promotion with id: {id} not found"); }
        _context.Promotions.Remove(promotion);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<PromotionDTO> GetById(int id)
    {
        var promotions = await _context.Promotions
            .Include(a => a.PromotionsEnterprises)
            .ThenInclude(a =>a.Enterprise)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (promotions is null) throw new NotFoundException($"The promotion with id: {id} doest not exist");

        return promotions.Adapt<PromotionDTO>();
    }

    public async Task<List<PromotionDTO>> GetFiltered(FilterPromotionModel filter)
    {
        var query = _context.Promotions
                                 .Include(a => a.PromotionsEnterprises)
                                 .ThenInclude(a => a.Enterprise)
                                 .AsQueryable();

        if (filter.Name is not null)
        {
            string normalizedFilterName = filter.Name.ToLower();
            query = query.Where(x =>
                (x.Name).ToLower().Equals(normalizedFilterName));
        }
       if (filter.PromotionStart is not null)
        {
            query = query.Where(x =>
                x.Start != null &&
                x.Start.Year >= filter.PromotionStart);
        }
        if (filter.PromotionEnd is not null)
        {
            query = query.Where(x =>
                x.End != null &&
                x.End.Year <= filter.PromotionEnd);
        }

        var result = await query.ToListAsync();
        if (result == null) { throw new NotFoundException("Promotions not found"); }
        var promotionDTO = result.Adapt<List<PromotionDTO>>();
        return promotionDTO;
    }

    public async Task<PromotionDTO> Update(UpdatePromotionModel model)
    {
        var query = _context.Promotions
                         .Include(a => a.PromotionsEnterprises)
                         .ThenInclude(a => a.Enterprise)
                         .AsQueryable();

        var result = await query.ToListAsync();

        var promotion = await _context.Promotions
        .Include(p => p.PromotionsEnterprises)
        .FirstOrDefaultAsync(p => p.Id == model.Id);

        if (promotion == null)
        {
            throw new NotFoundException("Enterprises not found");
        }

        model.Adapt(promotion);

        promotion.PromotionsEnterprises.Clear();

        foreach (int enterpriseId in model.Enterprises)
        {
            var promotionEnterprise = new PromotionEnterprise
            {
                PromotionId = promotion.Id,
                EnterpriseId = enterpriseId
            };
            promotion.PromotionsEnterprises.Add(promotionEnterprise);
        }

        await _context.SaveChangesAsync();
        var promotionDTO = promotion.Adapt<PromotionDTO>();
        return promotionDTO;
    }
}
