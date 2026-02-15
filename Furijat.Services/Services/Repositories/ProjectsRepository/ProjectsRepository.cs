using AutoMapper;
using AutoMapper.QueryableExtensions;
using Furijat.Data.Data;
using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Data.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Services.Services.Repositories.ProjectsRepository;

public class ProjectsRepository : IProjectsRepository
{

    private IMapper _mapper;
    private DataContext _db;
    private IWebHostEnvironment _hostenv;
    
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
        return await _db.Projects.Include(p => p.User).ProjectTo<ProjectResponseDTO>(_mapper.ConfigurationProvider).FirstAsync(p => p.Id == Guid.Parse(projectid));
    }

    public async Task<Project> GetProjectDirect(string projectid)
    {
        return await _db.Projects.Include(p => p.User).FirstAsync(p => p.Id == Guid.Parse(projectid));
    }

    public async Task<bool> AddProject(ProjectRequestDTO projecttoadd)
    {
        Project newproject = _mapper.Map<Project>(projecttoadd);
        Directory.CreateDirectory(Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects", $"{newproject.Id}", "Images")); 
        foreach (var imagefile in projecttoadd.ImagesFiles)
        {
            bool checkimg = await AddProjectImage(newproject.Id.ToString(),imagefile);
            if (checkimg) newproject.Imagesnames.Append(imagefile.FileName);
        }
        await _db.Projects.AddAsync(newproject);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateProject(ProjectRequestDTO projecttoupdate)
    {
        Project selectedproject = await _db.Projects.FirstAsync(p => p.Id == projecttoupdate.Id);
        selectedproject = _mapper.Map<Project>(projecttoupdate);
        //check images names and files
        foreach (var imgfile in projecttoupdate.ImagesFiles)
        {
            bool found = false;
            for (int i = 0; i < selectedproject.Imagesnames.Length; i++)
            {
                if (selectedproject.Imagesnames[i] == imgfile.FileName)
                {
                    found = true;
                }
            }

            if (!found)
            {
                bool check = await AddProjectImage(selectedproject.Id.ToString(), imgfile);
                if (check) selectedproject.Imagesnames.Append(imgfile.FileName);
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
            var check = _db.Projects.Remove(selectedproject);
            await _db.SaveChangesAsync();
            return true;
        }
        else if (selectedproject == null)
        {
            return true;
        }
        else return false;
    }
    

    public async Task CreateFolders()
    {
        try
        {
            List<Project> allprojects = await _db.Projects.ToListAsync();
            foreach (Project project in allprojects)
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
    
    private async Task<bool> AddProjectImage(string projectid,IFormFile imgfile)
    {
        int retry = 0;
        bool finalcheck = false;
        var filetocreate = Path.Combine(_hostenv.ContentRootPath, "Storage", "Projects", $"{projectid}", "Images", $"{imgfile.FileName}");
        if (File.Exists(filetocreate))
        {
            finalcheck = true;
        }
        else
        {
                var stream = new FileStream(filetocreate, FileMode.Create);
                var check =  imgfile.CopyToAsync(stream).IsCompletedSuccessfully;
                if (check)
                {
                    return true;
                }
                else
                {
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
        }
        return finalcheck;
    }

}