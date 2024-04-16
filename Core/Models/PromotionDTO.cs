using Core.Entities;

namespace Core.Models;

public class PromotionDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Discount { get; set; }
    public List<EnterpriseDTO> Enterprises { get; set; } = new List<EnterpriseDTO>();

}
