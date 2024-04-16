using Core.Entities;

namespace Core.Requests;

public class CreatePromotionEnterprise
{
    public List<int> Enterprises {get; set; } = new List<int>();
}
