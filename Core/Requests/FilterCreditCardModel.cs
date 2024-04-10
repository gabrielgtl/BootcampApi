using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests;

public class FilterCreditCardModel
{
    public int? CustomerId {  get; set; }
    public string Designation { get; set; } = string.Empty;
}
