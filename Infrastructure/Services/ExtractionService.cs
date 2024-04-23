using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Extraction;

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
        return await _extractionRepository.Extraction(model);
    }
}
