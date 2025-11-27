using FluentValidation;
using KadicNotificationApi.Application.DTOs.DeleteDto;

namespace KadicNotificationApi.Application.Validators.EmailTypeValidators;
public class EmailTypeDeleteValidator : AbstractValidator<EmailTypeDeleteDto>
{
    public EmailTypeDeleteValidator() 
    {
        RuleFor(x => x.Id)
        .GreaterThan(0).WithMessage("La solicitud contiene datos inválidos.");
    }
}