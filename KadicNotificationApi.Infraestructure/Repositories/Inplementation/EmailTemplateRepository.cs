using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KadicNotificationApi.Infraestructure.Repositories.Implementation
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly NotificationDbContext _db;

     
        public EmailTemplateRepository(NotificationDbContext db)
        {
            _db = db;
        }      

        public async Task<EmailTemplate> SendByIdAsync(int id)
        {
            var data = await _db.EmailTemplate
                .AsNoTracking()
                .Include(t => t.Type)
                .Include(t => t.Subjet)
                .FirstOrDefaultAsync(t => t.Id == id);

            return data;
        }

    }
}