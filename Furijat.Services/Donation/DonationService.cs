using AutoMapper;
using AutoMapper.QueryableExtensions;
using Furijat.Data;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Mail;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Services.Donation;

public class DonationService : IDonationService
{
    private readonly DataContext _db;
    private readonly IMail _mailservice;
    private readonly IMapper _mapper;
    private readonly IProjectsRepository _projectsrepo;
    private readonly IUserRepository _usersrepo;

    public Donate(IProjectsRepository projectsrepo, IMapper mapper, IUserRepository usersrepo, DataContext db, IMail mailservice)
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
        var querieddonationlog =
            await _db.DonationLogs.Include(d => d.Project).Include(d => d.User).FirstAsync(d => d.Id == Guid.Parse(donationid));
        var queriedproject = await _projectsrepo.GetProjectDirect(querieddonationlog.ProjectId.ToString());
        var donator = await _db.Users.FirstAsync(u => u.Id == querieddonationlog.UserId);

        if (decision)
        {
            querieddonationlog.Status = true;
            _db.DonationLogs.Update(querieddonationlog);
            await _db.SaveChangesAsync();
            await _mailservice.MailNotifyApproveDonator(querieddonationlog, donator, queriedproject);
            return true;
        }

        if (!decision)
        {
            querieddonationlog.Status = false;
            _db.DonationLogs.Update(querieddonationlog);
            await _db.SaveChangesAsync();
            await _mailservice.MailNotifyRejectDonator(querieddonationlog, donator, queriedproject);
            return true;
        }

        return false;
    }

    public async Task<bool> DonateToProject(DonationRequestDTO donationtolog)
    {
        var donator = await _usersrepo.GetUserDirect(donationtolog.UserId.ToString());
        var project = await _projectsrepo.GetProjectDirect(donationtolog.ProjectId.ToString());

        var newdonationlog = new Data.Models.Donation
        {
            Id = Guid.NewGuid(),
            UserId = donator.Id,
            User = donator,
            ProjectId = project.Id,
            Project = project,
            DonationAmount = donationtolog.DonationAmount,
            Date = DateOnly.FromDateTime(DateTime.Now),
            PaymentType = donationtolog.PaymentType
        };
        await _db.DonationLogs.AddAsync(newdonationlog);
        await _mailservice.MailNotifyProjectOwner(project.User.Email, project, newdonationlog);
        await _mailservice.MailNotifyDonator(donator.Email, project, newdonationlog);
        await _db.SaveChangesAsync();
        return true;
    }
}