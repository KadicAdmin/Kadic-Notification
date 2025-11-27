using KadicNotificationApi.Domain.Entities;
namespace KadicNotificationApi.Infraestructure.Repository.Interface;
public interface IPdfFileRepository
{
    Task<PdfFile?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(PdfFile entity, CancellationToken cancellationToken = default);
}
