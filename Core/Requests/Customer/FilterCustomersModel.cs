namespace Core.Requests.Customer;

public class FilterCustomersModel
{
    public string? FullName { get; set; }
    public int? BirthYearFrom { get; set; }
    public int? BirthYearTo { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Mail { get; set; }
    public int? BankId { get; set; }
}