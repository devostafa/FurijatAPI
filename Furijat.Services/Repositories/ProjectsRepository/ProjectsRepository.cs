using AutoMapper;
using AutoMapper.QueryableExtensions;
using Furijat.Data;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Furijat.Services.Repositories.ProjectsRepository;

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

    public async Task<List<ProjectResponseDTO>> GetProjects()
    {
        return await _db.Projects.ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    /*
    public async Task<List<Project>> GetProjectsOfCategory(string categoryid)
    {
        return await _db.Projects.Where(x => x.Category.Id == Guid.Parse(categoryid)).ToListAsync();
    }
    */

    public async Task<ProjectResponseDTO> GetProject(string projectid)
    {
        return await _db.Projects.Include(p => p.User).ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider)
            .FirstAsync(p => p.Id == Guid.Parse(projectid));
    }

    public async Task<Project> GetProjectDirect(string projectid)
    {
        return await _db.Projects.Include(p => p.User).FirstAsync(p => p.Id == Guid.Parse(projectid));
    }

    public async Task<bool> AddProject(ProjectRequestDTO projecttoadd)
    {
        var newproject = _mapper.Map<Project>(projecttoadd);
        Directory.CreateDirectory(Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects", $"{newproject.Id}", "Images"));

        foreach (var imagefile in projecttoadd.ImagesFiles)
        {
            var checkimg = await AddProjectImage(newproject.Id.ToString(), imagefile);
            if (checkimg) newproject.ImageNames.Append(imagefile.FileName);
        }

        await _db.Projects.AddAsync(newproject);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateProject(ProjectRequestDTO projecttoupdate)
    {
        var selectedproject = await _db.Projects.FirstAsync(p => p.Id == projecttoupdate.Id);
        selectedproject = _mapper.Map<Project>(projecttoupdate);

        //check images names and files
        foreach (var imgfile in projecttoupdate.ImagesFiles)
        {
            var found = false;

            for (var i = 0; i < selectedproject.ImageNames.Length; i++)
            {
                if (selectedproject.ImageNames[i] == imgfile.FileName)
                {
                    found = true;
                }
            }

            if (!found)
            {
                var check = await AddProjectImage(selectedproject.Id.ToString(), imgfile);
                if (check) selectedproject.ImageNames.Append(imgfile.FileName);
            }
        }

        _db.Projects.Update(selectedproject);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveProject(string projectid)
    {
        var selectedproject = await _db.Projects.FindAsync(Guid.Parse(projectid));

        if (selectedproject != null)
        {
            EntityEntry<Project> check = _db.Projects.Remove(selectedproject);
            await _db.SaveChangesAsync();
            return true;
        }

        if (selectedproject == null)
        {
            return true;
        }

        return false;
    }


    public async Task CreateFolders()
    {
        try
        {
            List<Project> allprojects = await _db.Projects.ToListAsync();

            foreach (var project in allprojects)
            {
                var productfoldertocreate = Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects",
                    $"{project.Id}", "Images");
                Directory.CreateDirectory(productfoldertocreate);
            }

            Console.WriteLine("Created Products assets folders successfully");
        }
        catch (Exception err)
        {
            throw err;
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