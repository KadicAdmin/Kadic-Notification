using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using KadicTechnology.CommonLib.Paginator;
using KadicTechnology.CommonLib.Utils;
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
    public async Task<PaginatorResponseDto<EmailTemplate>> GetByTenantAsync(int tenantId, PaginatorRequestDto paginatorRequestDto)
    {

        int page = 0;
        int pageSize = paginatorRequestDto.PageSize ?? 25;

        if (paginatorRequestDto.Page == null || paginatorRequestDto.Page == 0)
        {
            page = 1;
        }
        var query = _db.EmailTemplates.AsNoTracking().Where(et => et.TenantId == tenantId);
        int totalRecords = await query.CountAsync();

        var skipRecords = page * pageSize;

        if (skipRecords >= totalRecords)
        {
            skipRecords = 0;
        }

        var data = await query
            .Skip(skipRecords)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatorResponseDto<EmailTemplate>
            {
            Data = data,           
            PageSize = pageSize
        };

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