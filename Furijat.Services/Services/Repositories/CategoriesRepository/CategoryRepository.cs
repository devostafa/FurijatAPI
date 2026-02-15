using AutoMapper;
using AutoMapper.QueryableExtensions;
using Furijat.Data.Data;
using Furijat.Data.Data.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Services.Services.Repositories.CategoriesRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly IWebHostEnvironment _hostenv;
    private readonly IMapper _mapper;
    private readonly DataContext _db;

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