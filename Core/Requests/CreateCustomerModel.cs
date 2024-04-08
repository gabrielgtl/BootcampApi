﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests;

public class CreateCustomerModel
{
    public string Name { get; set; } = string.Empty;
    public string? Lastname { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string Mail { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string CustomerStatus { get; set; } = string.Empty;
    public DateTime? Birth { get; set; }
    public int BankId { get; set; }

}
