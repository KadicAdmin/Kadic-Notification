using KadicNotificationApi.Application.DTOs.MediaDto;
using KadicNotificationApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Application.Services.Implementation
{
    // Asegúrate de usar el namespace correcto donde está tu IMediaService y DTOs
    // using Kadic.Media.Application.Interfaces;
    // using Kadic.Media.Application.DTOs;

    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        // ===================== PDF =====================

        /// <summary>
        /// Sube un PDF y lo guarda en la base de datos.
        /// </summary>
        /// <param name="file">Archivo PDF enviado en form-data con key "file".</param>
        /// <param name="title">Título opcional del PDF.</param>
        [HttpPost("pdf")]
        [RequestSizeLimit(100_000_000)] // 100 MB
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000)]
        public async Task<IActionResult> UploadPdf(
            IFormFile file,
            [FromForm] string? title,
            CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("El archivo PDF está vacío.");

            if (!string.Equals(file.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                // Si quieres ser flexible, puedes quitar esta validación o solo loguearla.
                return BadRequest("El archivo debe ser un PDF (content-type application/pdf).");
            }

            var fileName = file.FileName;
            var effectiveTitle = string.IsNullOrWhiteSpace(title)
                ? Path.GetFileNameWithoutExtension(fileName)
                : title;

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms, cancellationToken);
                fileBytes = ms.ToArray();
            }

            var uploadDto = new MediaUploadDto
            {
                Title = effectiveTitle,
                FileName = fileName,
                ContentType = file.ContentType,
                FileData = fileBytes
            };

            var id = await _mediaService.SavePdfAsync(uploadDto, cancellationToken);

            // Construimos URL absoluta para que luego puedas usarla en Twilio.
            var url = Url.Action(
                action: nameof(GetPdf),
                controller: "Media",
                values: new { id },
                protocol: Request.Scheme);

            return Created(url!, new { Id = id, Url = url });
        }

        /// <summary>
        /// Devuelve un PDF para descargar (usado por navegador o Twilio).
        /// </summary>
        [HttpGet("pdf/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPdf(int id, CancellationToken cancellationToken)
        {
            var media = await _mediaService.GetPdfForDownloadAsync(id, cancellationToken);
            if (media is null)
                return NotFound();

            return File(
                fileContents: media.FileData,
                contentType: media.ContentType,
                fileDownloadName: media.FileName);
        }

        // ===================== VIDEO =====================

        /// <summary>
        /// Sube un video (mp4) y lo guarda en la base de datos.
        /// </summary>
        /// <param name="file">Archivo de video enviado en form-data con key "file".</param>
        /// <param name="title">Título opcional del video.</param>
        [HttpPost("video")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadVideo(
            IFormFile file,
            [FromForm] string? title,
            CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("El archivo de video está vacío.");

            // Puedes validar por ContentType si quieres restringir a mp4:
            // if (!file.ContentType.StartsWith("video/"))
            //     return BadRequest("El archivo debe ser un video válido.");

            var fileName = file.FileName;
            var effectiveTitle = string.IsNullOrWhiteSpace(title)
                ? Path.GetFileNameWithoutExtension(fileName)
                : title;

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms, cancellationToken);
                fileBytes = ms.ToArray();
            }

            var uploadDto = new MediaUploadDto
            {
                Title = effectiveTitle,
                FileName = fileName,
                ContentType = file.ContentType, // Ej: "video/mp4"
                FileData = fileBytes
            };

            var id = await _mediaService.SaveVideoAsync(uploadDto, cancellationToken);

            var url = Url.Action(
                action: nameof(GetVideo),
                controller: "Media",
                values: new { id },
                protocol: Request.Scheme);

            return Created(url!, new { Id = id, Url = url });
        }

        /// <summary>
        /// Devuelve un video para descargar (usado por navegador o Twilio).
        /// </summary>
        [HttpGet("video/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVideo(int id, CancellationToken cancellationToken)
        {
            var media = await _mediaService.GetVideoForDownloadAsync(id, cancellationToken);
            if (media is null)
                return NotFound();

            return File(
                fileContents: media.FileData,
                contentType: media.ContentType,
                fileDownloadName: media.FileName);
        }
    }

}
