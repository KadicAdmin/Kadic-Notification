using FluentValidation;
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.Interfaces;
using KadicNotificationApi.Infraestructure.Repositories.Interfaces;

namespace KadicNotificationApi.Application.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IValidator<EmailTemplateDto> _validator;

        public EmailTemplateService(
            IEmailTemplateRepository emailTemplateRepository,
            IValidator<EmailTemplateDto> validator)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _validator = validator;
        }

        public async Task SendById(EmailTemplateDto emailTemplateDto)
        {
            var result = await _emailTemplateRepository.SendByIdAsync(emailTemplateDto);
            if (result == null)
            {
                
            }
        }
    }
}