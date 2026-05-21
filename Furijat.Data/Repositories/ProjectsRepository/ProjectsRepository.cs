using AutoMapper;
using AutoMapper.QueryableExtensions;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Enums;
using Furijat.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Furijat.Data.Repositories.ProjectsRepository;

public class ProjectsRepository : IProjectsRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnv;

    public ProjectsRepository(DataContext db, IMapper mapper, IWebHostEnvironment webHostEnv)
    {
        _mapper = mapper;
        _db = db;
        _webHostEnv = webHostEnv;
    }

    public async Task<PaginatedProjectsResponseDTO> GetProjectsAsync(int? pageNumber, string? categoryId)
    {
        const int itemsPerPage = 10;
        var actualPage = pageNumber ?? 1;
        if (actualPage < 1) actualPage = 1;

        IQueryable<Project> query = _db.Projects.AsQueryable();

        if (!string.IsNullOrEmpty(categoryId))
        {
            query = query.Where(p => p.Category.Id.ToString() == categoryId);
        }

        var totalProjects = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalProjects / itemsPerPage);

        List<ProjectResponseDTO> projects = await query
            .Skip((actualPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedProjectsResponseDTO(totalPages, projects);
    }

    public async Task<ProjectResponseDTO> GetProjectAsync(string projectId)
    {
        return await _db.Projects.Include(p => p.User).ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider)
            .FirstAsync(p => p.Id == Guid.Parse(projectId));
    }

    public async Task<bool> AddProjectAsync(ProjectRequestDTO newProjectRequest)
    {
        var newproject = _mapper.Map<Project>(newProjectRequest);
        Directory.CreateDirectory(Path.Combine(_webHostEnv.ContentRootPath, "Storage", "Projects", $"{newproject.Id}", "Images"));

        foreach (var imagefile in newProjectRequest.ImagesFiles)
        {
            var checkimg = await AddProjectImage(newproject.Id.ToString(), imagefile);
            if (checkimg) newproject.ImagesNames.Add(imagefile.FileName);
        }

        await _db.Projects.AddAsync(newproject);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateProjectAsync(ProjectRequestDTO projectUpdateRequest)
    {
        var project = await _db.Projects.FirstAsync(p => p.Id == projectUpdateRequest.Id);
        project = _mapper.Map<Project>(projectUpdateRequest);

        //check images names and files
        foreach (var imgfile in projectUpdateRequest.ImagesFiles)
        {
            var found = false;

            for (var i = 0; i < project.ImagesNames.Count; i++)
            {
                if (project.ImagesNames[i] == imgfile.FileName)
                {
                    found = true;
                }
            }

            if (!found)
            {
                var check = await AddProjectImage(project.Id.ToString(), imgfile);
                if (check) project.ImagesNames.Add(imgfile.FileName);
            }
        }

        _db.Projects.Update(project);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateProjectStatusAsync(string projectId, ProjectStatusEnum statusUpdate)
    {
        var project = await _db.Projects.FirstAsync(p => p.Id == Guid.Parse(projectId));

        project.Status = statusUpdate;

        _db.Projects.Update(project);

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateProjectLikes(string projectId)
    {
        var project = await _db.Projects.FirstAsync(p => p.Id == Guid.Parse(projectId));

        project.Likes++;

        _db.Projects.Update(project);

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveProjectAsync(string projectId)
    {
        var project = await _db.Projects.FindAsync(Guid.Parse(projectId));

        if (project != null)
        {
            EntityEntry<Project> check = _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
            return true;
        }

        return project == null;
    }


    public async Task CreateFoldersAsync()
    {
        try
        {
            List<Project> allProjects = await _db.Projects.ToListAsync();

            foreach (var project in allProjects)
            {
                var productfoldertocreate = Path.Combine(_webHostEnv.ContentRootPath, "Storage", "Projects",
                    $"{project.Id}", "Images");
                Directory.CreateDirectory(productfoldertocreate);
            }

            Console.WriteLine("Created Products assets folders successfully");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create projects folders", ex);
        }
    }

    private async Task<bool> AddProjectImage(string projectId, IFormFile imgFile)
    {
        var retry = 0;
        var finalCheck = false;
        var imgFileToCreate = Path.Combine(_webHostEnv.ContentRootPath, "Storage", "Projects", $"{projectId}", "Images", $"{imgFile.FileName}");

        if (File.Exists(imgFileToCreate))
        {
            finalCheck = true;
        }
        else
        {
            var stream = new FileStream(imgFileToCreate, FileMode.Create);
            var check = imgFile.CopyToAsync(stream).IsCompletedSuccessfully;

            if (check)
            {
                return true;
            }

            if (retry > 1)
            {
                finalCheck = false;
            }
            else
            {
                retry += 1;
                AddProjectImage(projectId, imgFile);
            }
        }

        return finalCheck;
    }
}