using Core.Models;
using Core.Requests.Extraction;

namespace Core.Interfaces.Services;

public interface IExtractionService
{
    Task<ExtractionDTO> Extraction(CreateExtractionModel model);

}
