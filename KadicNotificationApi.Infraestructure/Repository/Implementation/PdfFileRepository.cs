using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace KadicNotificationApi.Infraestructure.Repository.Implementation
{
    public class PdfFileRepository : IPdfFileRepository
    {
        private readonly NotificationDbContext _dbContext;

        public PdfFileRepository(NotificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PdfFile?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.PdfFiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<int> AddAsync(PdfFile entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.PdfFiles.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
