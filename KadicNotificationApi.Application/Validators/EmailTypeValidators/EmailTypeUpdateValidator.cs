using FluentValidation;
using KadicNotificationApi.Application.DTOs.UpdateDto;

namespace KadicNotificationApi.Application.Validators.EmailTypeValidators;

public class EmailTypeUpdateValidator : AbstractValidator<EmailTypeUpdateDto>
{
    public EmailTypeUpdateValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("La solicitud contiene datos inválidos.");
        RuleFor(x => x.HtmlBody)
            .NotEmpty().WithMessage("Este campo es obligatorio.");
    }
}