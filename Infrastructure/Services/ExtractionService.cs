using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Extraction;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ExtractionService : IExtractionService
{
    private readonly IExtractionRepository _extractionRepository;

    public ExtractionService(IExtractionRepository extractionRepository)
    {
        _extractionRepository = extractionRepository;
    }

    public async Task<ExtractionDTO> Extraction(CreateExtractionModel model)
    {
        var validationResult = await _extractionRepository.DataValidation(model);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _extractionRepository.Extraction(model);
    }
}
