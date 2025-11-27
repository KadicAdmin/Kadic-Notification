using KadicNotificationApi.Application.DTOs.MediaDto;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Application.Services.Implementation
{
    public class MediaService : IMediaService
    {
        private readonly IPdfFileRepository _pdfRepo;
        private readonly IVideoFileRepository _videoRepo;

        public MediaService(
            IPdfFileRepository pdfRepo,
            IVideoFileRepository videoRepo)
        {
            _pdfRepo = pdfRepo;
            _videoRepo = videoRepo;
        }


        public async Task<MediaDownloadDto?> GetPdfForDownloadAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var pdf = await _pdfRepo.GetByIdAsync(id, cancellationToken);

            if (pdf is null)
                return null;

            return new MediaDownloadDto
            {
                FileName = pdf.FileName,
                ContentType = pdf.ContentType,
                FileData = pdf.FileData
            };
        }

        public async Task<MediaDownloadDto?> GetVideoForDownloadAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var video = await _videoRepo.GetByIdAsync(id, cancellationToken);

            if (video is null)
                return null;

            return new MediaDownloadDto
            {
                FileName = video.FileName,
                ContentType = video.ContentType,
                FileData = video.FileData
            };
        }

        // ========== GUARDAR ==========

        public Task<int> SavePdfAsync(MediaUploadDto dto, CancellationToken cancellationToken = default)
        {
            var entity = new PdfFile
            {
                Title = dto.Title,
                FileName = dto.FileName,
                ContentType = string.IsNullOrWhiteSpace(dto.ContentType)
                    ? "application/pdf"
                    : dto.ContentType,
                FileData = dto.FileData,
                CreatedAt = DateTime.UtcNow
            };

            return _pdfRepo.AddAsync(entity, cancellationToken);
        }

        public Task<int> SaveVideoAsync(MediaUploadDto dto, CancellationToken cancellationToken = default)
        {
            var entity = new VideoFile
            {
                Title = dto.Title,
                FileName = dto.FileName,
                ContentType = string.IsNullOrWhiteSpace(dto.ContentType)
                    ? "video/mp4"
                    : dto.ContentType,
                FileData = dto.FileData,
                CreatedAt = DateTime.UtcNow
            };

            return _videoRepo.AddAsync(entity, cancellationToken);
        }
    }
}
