using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Infraestructure.Repository.Implementation
{
    public class VideoFileRepository : IVideoFileRepository
    {
        private readonly NotificationDbContext _dbContext;

        public VideoFileRepository(NotificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<VideoFile?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.VideoFiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> AddAsync(VideoFile entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.VideoFiles.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
