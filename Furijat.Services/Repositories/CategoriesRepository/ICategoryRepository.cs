using Furijat.Data.DTOs.ResponseDTO;

namespace Furijat.Services.Repositories.CategoriesRepository;

public interface ICategoryRepository
{
    public Task<List<CategoryResponseDTO>> GetCategories();
}