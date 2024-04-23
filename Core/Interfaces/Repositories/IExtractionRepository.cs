using Core.Models;
using Core.Requests.Extraction;

namespace Core.Interfaces.Repositories;

public interface IExtractionRepository
{
    Task<ExtractionDTO> Extraction(CreateExtractionModel model);
    Task<(bool isValid, string message)> DataValidation(CreateExtractionModel model);
}
