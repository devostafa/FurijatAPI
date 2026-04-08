using AutoMapper;
using Furijat.Data;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Mail;

namespace Furijat.Services.Donation;

public class DonationService : IDonationService
{
    private readonly DataContext _db;
    private readonly IMail _mailService;
    private readonly IMapper _mapper;
    private readonly IProjectsRepository _projectsRepo;
    private readonly IUserRepository _usersRepo;

    public DonationService(IProjectsRepository projectsRepo, IMapper mapper, IUserRepository usersRepo, DataContext db, IMail mailService)
    {
        _projectsRepo = projectsRepo;
        _usersRepo = usersRepo;
        _db = db;
        _mapper = mapper;
        _mailService = mailService;

        throw new NotImplementedException("Donation feature needs to be re-thought about");
    }

    /*

    public async Task<List<DonationResponseDTO>> GetDonations()
    {
        return await _db.DonationLogs.ProjectTo<DonationResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> DecideDonation(string donationid, bool decision)
    {
        var donationLogResponse = await _db.DonationLogs.Include(d => d.Project)
                                                                .Include(d => d.User)
                                                                .FirstAsync(d => d.Id == Guid.Parse(donationid));

        var projectResponse = await _projectsRepo.GetProjectAsync(donationLogResponse.ProjectId.ToString());

        var donator = await _db.Users.FirstAsync(u => u.Id == donationLogResponse.UserId);

        if (decision)
        {
            donationLogResponse.Status = true;
            _db.DonationLogs.Update(donationLogResponse);
            await _db.SaveChangesAsync();
            await _mailService.SendMailAsync(donationLogResponse, donator, queriedproject);
            return true;
        }

        if (!decision)
        {
            donationLogResponse.Status = false;
            _db.DonationLogs.Update(donationLogResponse);
            await _db.SaveChangesAsync();
            await _mailService.MailNotifyRejectDonator(donationLogResponse, donator, queriedproject);
            return true;
        }

        return false;
    }

    public async Task<bool> DonateToProject(DonationRequestDTO donationtolog)
    {
        var donator = await _usersRepo.GetUserDirect(donationtolog.UserId.ToString());
        var project = await _projectsRepo.GetProjectDirect(donationtolog.ProjectId.ToString());

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
        await _mailService.MailNotifyProjectOwner(project.User.Email, project, newdonationlog);
        await _mailService.MailNotifyDonator(donator.Email, project, newdonationlog);
        await _db.SaveChangesAsync();
        return true;
    }

     */
}