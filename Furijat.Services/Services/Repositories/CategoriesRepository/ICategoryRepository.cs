using Furijat.Data.Data.DTOs.ResponseDTO;

namespace Furijat.Services.Services.Repositories.CategoriesRepository;

public interface ICategoryRepository
{
    public Task<List<CategoryResponseDTO>> GetCategories();
}