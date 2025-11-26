using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using KadicTechnology.CommonLib.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace KadicNotificationApi.Infraestructure.Repository.Implementation;

public class EmailTypeRepository : IEmailTypeRepository
{
    private readonly NotificationDbContext _db;

    public EmailTypeRepository(NotificationDbContext db)
    {
        _db = db;
    }

    public async Task<EmailTemplateType?> GetByIdAsync(int id)
    {
        return await _db.EmailTemplateTypes
            .AsNoTracking()
            .FirstAsync(t => t.Id == id);
    }
    public async Task<PaginatorResponseDto<EmailTemplateType>> GetAllAsync(PaginatorRequestDto paginatorRequestDto)
    {
        int page = 0;
        int pageSize = paginatorRequestDto.PageSize ?? 25;

        if (paginatorRequestDto.Page == null || paginatorRequestDto.Page == 0)
        {
            page = 1;
        }
        var query = from EmailTemplateTypes in _db.Set<EmailTemplateType>()
                    select EmailTemplateTypes;
        var sortField = paginatorRequestDto.SortField == null ? "Id" : paginatorRequestDto.SortField.ToLower();
        if (!String.IsNullOrEmpty(sortField))
        {
            bool isDescending = paginatorRequestDto.SortOrder?.ToLower() == "desc";
            var sortingFields = new Dictionary<string, Func<IQueryable<EmailTemplateType>, IQueryable<EmailTemplateType>>>
            {
                { "id", q => isDescending ? q.OrderByDescending(e => e.Id) : q.OrderBy(e => e.Id) },
                { "htmlbody", q => isDescending ? q.OrderByDescending(e => e.HtmlBody) : q.OrderBy(e => e.HtmlBody) }
            };

            if (sortingFields.ContainsKey(sortField))
            {
                query = sortingFields[sortField](query);
            }

            else
            {
                string sortOrder = isDescending ? "descending" : "ascending";
                query = query.OrderBy($"{sortField} {sortOrder}");

            }
        }
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

        return new PaginatorResponseDto<EmailTemplateType>
        {
            Data = data,
            TotalRecords = totalRecords
        };

    }
    public async Task SaveAsync(EmailTemplateType emailTemplateType)
    {
        await _db.AddAsync(emailTemplateType);
        await _db.SaveChangesAsync();
    }
    public async Task UpdateAsync(EmailTemplateType emailTemplateType)
    {
        _db.EmailTemplateTypes.Update(emailTemplateType);
        await _db.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _db.EmailTemplateTypes.FirstAsync(t => t.Id == id);
        _db.EmailTemplateTypes.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
