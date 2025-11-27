using KadicNotificationApi.Application.DTOs.MediaDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Application.Services.Interfaces;
    public interface IMediaService
    {
        // DESCARGAR
        Task<MediaDownloadDto?> GetPdfForDownloadAsync(int id, CancellationToken cancellationToken = default);
        Task<MediaDownloadDto?> GetVideoForDownloadAsync(int id, CancellationToken cancellationToken = default);

        // GUARDAR
        Task<int> SavePdfAsync(MediaUploadDto dto, CancellationToken cancellationToken = default);
        Task<int> SaveVideoAsync(MediaUploadDto dto, CancellationToken cancellationToken = default);
    }
