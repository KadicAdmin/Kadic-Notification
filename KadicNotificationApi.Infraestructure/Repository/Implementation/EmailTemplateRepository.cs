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

    public async Task<EmailTemplate?> GetByIdAsync(int id, int tenantId)
    {
        return await _db.EmailTemplates.FindAsync(id, tenantId);
    }
    public async Task<IReadOnlyList<EmailTemplate>> GetByTenantAsync(int tenantId, int page, int pageSize)
    {      
            return await _db.EmailTemplates 
            .Where(t => t.TenantId == tenantId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();        
    }
    public async Task UpdateAsync(EmailTemplate template)
    {
        _db.EmailTemplates.Attach(template);

        var entry = _db.Entry(template);
        entry.State = EntityState.Modified;

        entry.Property(e => e.TenantId).IsModified = false;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(EmailTemplate template)
    {
        _db.EmailTemplates.Remove(template);
        await _db.SaveChangesAsync();
    }
    public async Task SaveAsync(EmailTemplate template)
    {
        await _db.EmailTemplates.AddAsync(template);
        await _db.SaveChangesAsync();
    }
}