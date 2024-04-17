namespace Core.Requests.Bank;

public class CreateBankModel
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Mail { get; set; }
    public string? Address { get; set; }
}