using FluentValidation;
using KadicNotificationApi.Application.DTOs.SaveDto;

namespace KadicNotificationApi.Application.Validators.SendEmailValidators
{
    public class SendTemplateValidator : AbstractValidator<SendTemplateSaveDto>
    {
        public SendTemplateValidator()
        {
            RuleFor(x => x.TemplateId)
                .GreaterThan(0).WithMessage("'templateId' debe ser mayor que cero.");

            RuleFor(x => x.To)
                .NotNull()
                .NotEmpty().WithMessage("Debe especificar al menos un destinatario.");                
        }
    }
}