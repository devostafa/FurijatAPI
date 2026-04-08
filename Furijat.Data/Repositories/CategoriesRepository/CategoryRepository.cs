using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;
using Microsoft.AspNetCore.Hosting;

namespace Furijat.Data.Repositories.CategoriesRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnv;

    public CategoryRepository(DataContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnv)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnv = webHostEnv;
    }

    public async Task<List<CategoryResponseDTO>> GetCategories()
    {
        return await _dbContext.Categories.ProjectTo<CategoryResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> AddCategoryAsync(string categoryName)
    {
        var newCategory = new Category
        {
            Id = Guid.NewGuid(), Name = categoryName
        };

        await _dbContext.Categories.AddAsync(newCategory);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}