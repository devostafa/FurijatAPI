using Furijat.Data.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Hosting;

namespace Furijat.Data.Repositories.CategoriesRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _hostenv;
    private readonly IMapper _mapper;

    public CategoryRepository(DataContext db, IMapper mapper, IWebHostEnvironment hostingEnvironment)
    {
        _db = db;
        _mapper = mapper;
        _hostenv = hostingEnvironment;
    }


    public async Task<List<CategoryResponseDTO>> GetCategories()
    {
        return await _db.Categories.ProjectTo<CategoryResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
}