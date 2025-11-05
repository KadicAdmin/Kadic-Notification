using FluentValidation;
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Validators
{
    public class EmailTemplateValidator : AbstractValidator<EmailTemplateDto>
    {
        public EmailTemplateValidator()
        {
            RuleFor(x => x.HtmlBody)
            .NotNull().WithMessage("'Type' es requerido.")
            .NotEmpty().WithMessage("'Type' no puede estar vacío.");

            RuleFor(x => x.Subjet)
           .NotNull().WithMessage("'Type' es requerido.")
           .NotEmpty().WithMessage("'Type' no puede estar vacío.");
        }

    }
}