using FluentValidation;
using KadicNotificationApi.Application.DTOs.SaveDto;

namespace KadicNotificationApi.Application.Validators.EmailTypeValidators;

public class EmailTypeSaveValidator : AbstractValidator<EmailTypeSaveDto>
{
    public EmailTypeSaveValidator() 
    {
        RuleFor(x => x.HtmlBody)
            .NotEmpty().WithMessage("Este campo es obligatorio.");
    }
}