using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
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

    public async Task<PromotionDTO> Update(UpdatePromotionModel model)
    {
        var promotion = await _context.Promotions.FindAsync(model.Id);

        if (promotion is null) throw new Exception("Promotion was not found");

        model.Adapt(promotion);

        _context.Promotions.Update(promotion);

        await _context.SaveChangesAsync();

        var promotionDTO = promotion.Adapt<PromotionDTO>();

        return promotionDTO;
    }
}
