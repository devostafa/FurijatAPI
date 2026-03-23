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
    private readonly IWebHostEnvironment _hostenv;

    private readonly IMapper _mapper;

    public ProjectsRepository(DataContext db, IMapper mapper, IWebHostEnvironment hostenv)
    {
        _mapper = mapper;
        _db = db;
        _hostenv = hostenv;
    }

    public async Task<List<ProjectResponseDTO>> GetProjectsAsync(string? categoryId)
    {
        return await _db.Projects.ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ProjectResponseDTO> GetProjectAsync(string projectid)
    {
        return await _db.Projects.Include(p => p.User).ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider)
            .FirstAsync(p => p.Id == Guid.Parse(projectid));
    }

    public async Task<bool> AddProjectAsync(ProjectRequestDTO newProjectRequest)
    {
        var newproject = _mapper.Map<Project>(newProjectRequest);
        Directory.CreateDirectory(Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects", $"{newproject.Id}", "Images"));

        foreach (var imagefile in newProjectRequest.ImagesFiles)
        {
            var checkimg = await AddProjectImage(newproject.Id.ToString(), imagefile);
            if (checkimg) newproject.ImageNames.Append(imagefile.FileName);
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

            for (var i = 0; i < project.ImageNames.Length; i++)
            {
                if (project.ImageNames[i] == imgfile.FileName)
                {
                    found = true;
                }
            }

            if (!found)
            {
                var check = await AddProjectImage(project.Id.ToString(), imgfile);
                if (check) project.ImageNames.Append(imgfile.FileName);
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
                var productfoldertocreate = Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects",
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


    private async Task<bool> AddProjectImage(string projectid, IFormFile imgfile)
    {
        var retry = 0;
        var finalcheck = false;
        var filetocreate = Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects", $"{projectid}", "Images", $"{imgfile.FileName}");

        if (File.Exists(filetocreate))
        {
            finalcheck = true;
        }
        else
        {
            var stream = new FileStream(filetocreate, FileMode.Create);
            var check = imgfile.CopyToAsync(stream).IsCompletedSuccessfully;

            if (check)
            {
                return true;
            }

            if (retry > 1)
            {
                finalcheck = false;
            }
            else
            {
                retry += 1;
                AddProjectImage(projectid, imgfile);
            }
        }

        return finalcheck;
    }
}