using AutoMapper;
using AutoMapper.QueryableExtensions;
using Furijat.Data.Data;
using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Data.Data.Models;
using Furijat.Services.Services.Mail;
using Furijat.Services.Services.Repositories.ProjectsRepository;
using Furijat.Services.Services.Repositories.UsersRepository;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Services.Services.Donate;

public class Donate : IDonate
{
    private readonly IProjectsRepository _projectsrepo;
    private readonly IUserRepository _usersrepo;
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    private readonly IMail _mailservice;

    public Donate(IProjectsRepository projectsrepo, IMapper mapper ,IUserRepository usersrepo, DataContext db, IMail mailservice)
    {
        _projectsrepo = projectsrepo;
        _usersrepo = usersrepo;
        _db = db;
        _mapper = mapper;
        _mailservice = mailservice;
    }

    public async Task<List<DonationResponseDTO>> GetDonations()
    {
        return await _db.DonationLogs.ProjectTo<DonationResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> DecideDonation(string donationid, bool decision)
    {
        Donation querieddonationlog = await _db.DonationLogs.Include(d => d.Project).Include(d => d.User).FirstAsync(d => d.Id == Guid.Parse(donationid));
        Project queriedproject = await _projectsrepo.GetProjectDirect(querieddonationlog.ProjectId.ToString());
        User donator = await _db.Users.FirstAsync(u => u.Id == querieddonationlog.UserId);

        if (decision)
        {
            querieddonationlog.Status = true;
            _db.DonationLogs.Update(querieddonationlog);
            await _db.SaveChangesAsync();
            await _mailservice.MailNotifyApproveDonator(querieddonationlog, donator, queriedproject);
            return true;
        }
        else if (!decision)
        {
            querieddonationlog.Status = false;
            _db.DonationLogs.Update(querieddonationlog);
            await _db.SaveChangesAsync();
            await _mailservice.MailNotifyRejectDonator(querieddonationlog, donator, queriedproject);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DonateToProject(DonationRequestDTO donationtolog)
    {
        User donator = await _usersrepo.GetUserDirect(donationtolog.UserId.ToString());
        Project project = await _projectsrepo.GetProjectDirect(donationtolog.ProjectId.ToString());
        Donation newdonationlog = new Donation
        {
            Id = Guid.NewGuid() , UserId = donator.Id, User = donator, ProjectId = project.Id, Project = project,
            DonationAmount = donationtolog.DonationAmount, Date = DateOnly.FromDateTime(DateTime.Now), PaymentType = donationtolog.PaymentType
        };
        await _db.DonationLogs.AddAsync(newdonationlog);
        await _mailservice.MailNotifyProjectOwner(project.User.Email, project, newdonationlog);
        await _mailservice.MailNotifyDonator(donator.Email, project, newdonationlog);
        await _db.SaveChangesAsync();
        return true;
    }



}