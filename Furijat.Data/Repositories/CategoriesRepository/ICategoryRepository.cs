using Furijat.Data.DTOs.ResponseDTO;

namespace Furijat.Data.Repositories.CategoriesRepository;

public interface ICategoryRepository
{
    public Task<List<CategoryResponseDTO>> GetCategories();

    public Task<bool> AddCategoryAsync(string categoryName);
}