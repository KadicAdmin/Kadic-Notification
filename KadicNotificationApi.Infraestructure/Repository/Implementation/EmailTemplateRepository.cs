using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace KadicNotificationApi.Infraestructure.Repository.Implementation;

public class EmailTemplateRepository : IEmailTemplateRepository
{
    private readonly NotificationDbContext _db;
    public EmailTemplateRepository(NotificationDbContext db) 
    {
        _db = db;
    }

    public Task<EmailTemplate?> GetByIdAsync(int id) => _db.EmailTemplates
           .AsNoTracking()
           .Include(t => t.TemplateType)   
           .SingleOrDefaultAsync(t => t.Id == id);
}