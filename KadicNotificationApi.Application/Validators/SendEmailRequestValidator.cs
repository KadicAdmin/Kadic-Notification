using FluentValidation;
using KadicNotificationApi.Application.DTOs;
namespace KadicNotificationApi.Application.Validators
{
    public class SendEmailRequestValidator : AbstractValidator<SendEmailRequest>
    {
        public SendEmailRequestValidator()
        {
            RuleFor(x => x.To)
            .NotNull().WithMessage("'to' es requerido.")
            .NotEmpty().WithMessage("'to' no puede estar vacío.");

            RuleForEach(x => x.To).ChildRules(r =>
            {
                r.RuleFor(y => y.Address)
                 .NotEmpty().WithMessage("La dirección de correo es requerida.")
                 .EmailAddress().WithMessage("La dirección de correo no es válida.");

                r.RuleFor(y => y.Name)
                 .MaximumLength(100).When(y => !string.IsNullOrWhiteSpace(y.Name));
            });

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("'subject' es requerido.")
                .MaximumLength(200);
            
            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.HtmlBody) || !string.IsNullOrWhiteSpace(x.TextBody))
                .WithMessage("Debes enviar 'htmlBody'");
        }
    }
}
