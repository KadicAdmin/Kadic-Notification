using KadicNotificationApi.Domain.Entities;
namespace KadicNotificationApi.Infraestructure.Repository.Interface;

public interface IVideoFileRepository
{
    Task<VideoFile?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(VideoFile entity, CancellationToken cancellationToken = default);
}