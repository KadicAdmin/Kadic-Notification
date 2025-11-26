using FluentValidation;
using KadicNotificationApi.Application.DTOs.DeleteDto;

namespace KadicNotificationApi.Application.Validators.EmailTemplateValidators
{
    public class EmailTemplateDeleteDtoValidator : AbstractValidator<EmailTemplateDeleteDto>
    {
        public EmailTemplateDeleteDtoValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("La solicitud contiene datos inválidos.");           
            RuleFor(x => x.TenantId)
                .GreaterThan(0).WithMessage("La solicitud contiene datos inválidos.");
        }
    }
}
