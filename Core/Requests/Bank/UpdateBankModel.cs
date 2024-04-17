namespace Core.Requests.Bank;

public class UpdateBankModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Mail { get; set; }
    public string? Address { get; set; }
}
