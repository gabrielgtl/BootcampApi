using Core.Models;
using Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface ICreditCardService
{
    Task<CreditCardDTO> Add(CreateCreditCardModel model);
    Task<List<CreditCardDTO>> GetFiltered(FilterCreditCardModel filter);
    Task<CreditCardDTO> Update(UpdateCreditCardModel model);
    Task<CreditCardDTO> GetById(int id);
    Task<bool> Delete(int id);
}
