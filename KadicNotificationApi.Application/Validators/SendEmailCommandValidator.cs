using FluentValidation;
using KadicNotificationApi.Application.DTOs;

namespace KadicNotificationApi.Application.Validators;
public class SendEmailCommandValidator : AbstractValidator<SendEmailRequest>
{
    public SendEmailCommandValidator()
    {
        RuleFor(x => x.To).NotEmpty().WithMessage("Debe especificar al menos un destinatario.");
        RuleForEach(x => x.To).ChildRules(a =>
        {
            a.RuleFor(y => y.Address).NotEmpty().EmailAddress();
        });

        RuleFor(x => x.Subject).NotEmpty().MaximumLength(255);

        RuleFor(x => x)
            .Must(x => !(string.IsNullOrWhiteSpace(x.HtmlBody) && string.IsNullOrWhiteSpace(x.TextBody)))
            .WithMessage("Debe enviar HtmlBody o TextBody.");

        RuleForEach(x => x.Attachments)
            .ChildRules(att =>
            {
                att.RuleFor(a => a.FileName).NotEmpty();
                att.RuleFor(a => a.ContentBase64).NotEmpty();
            })
            .When(x => x.Attachments != null && x.Attachments.Count > 0);
    }
}