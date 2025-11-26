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
        return await _db.EmailTemplates
            .AsNoTracking()
            .FirstOrDefaultAsync(et => et.Id == id && et.TenantId == tenantId);
    }
    public async Task<PaginatorResponseDto<EmailTemplate>> GetByTenantAsync(int tenantId, PaginatorRequestDto paginatorRequestDto)

    {
        var page = paginatorRequestDto.Page ?? 0;
        var pageSize = paginatorRequestDto.PageSize ?? 500;

        if (page < 0) page = 0;
        if (pageSize <= 0) pageSize = 500;

        var query = _db.EmailTemplates
            .AsNoTracking()
            .Where(et => et.TenantId == tenantId);

        var totalRecords = await query.CountAsync();

        var totalPages = totalRecords == 0
            ? 0
            : (int)Math.Ceiling(totalRecords / (double)pageSize);

        if (totalPages > 0 && page >= totalPages)
            page = totalPages - 1;

        var data = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatorResponseDto<EmailTemplate>
        {
            Data = data,
            TotalRecords = totalRecords,
            CurrentPage = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }
    public async Task UpdateAsync(EmailTemplate template)
    {
      _db.EmailTemplates.Update(template);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int tenantId)
    {
        var entity = await _db.EmailTemplates
        .FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
        if (entity == null)
        {
            throw new KeyNotFoundException("Registro No Encontrado");
        }
        _db.EmailTemplates.Remove(entity);
            await _db.SaveChangesAsync();
                    
    }
    public async Task SaveAsync(EmailTemplate template)
    {
        await _db.EmailTemplates.AddAsync(template);
        await _db.SaveChangesAsync();
    }
}