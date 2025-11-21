using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using KadicNotificationApi.Infraestructure.SMTP;

namespace KadicNotificationApi.Application.Services.Implementation;

public class SendTemplateService : ISendTemplateService
{
    private readonly ISendTemplateRepository _repo;
    private readonly SmtpEmailTemplate _smtp;

    public SendTemplateService(
        ISendTemplateRepository repo,
        SmtpEmailTemplate smtp)
    {
        _repo = repo;
        _smtp = smtp;
    }

    public async Task SendAsync(SendTemplateSaveDto request)
    {
        var clean = new List<string>();
        var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (request.To != null)
        {
            foreach (var raw in request.To)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var correo = raw.Trim();
                if (vistos.Add(correo))
                    clean.Add(correo);
            }
        }
        if (clean.Count == 0)
            throw new InvalidOperationException("Debe especificar al menos un destinatario.");


        var template = await _repo.GetByIdAsync(request.TemplateId);
        if (template == null)
            throw new InvalidOperationException("Plantilla no encontrada.");


        if (string.IsNullOrWhiteSpace(template.TemplateType?.HtmlBody))
            throw new InvalidOperationException("La plantilla no tiene HtmlBody.");

        await _smtp.SendEmailAsync(template, clean);
    }
}