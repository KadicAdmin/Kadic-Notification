using FluentValidation;
using KadicNotificationApi.Application.DTOs.SaveDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Application.Validators.EmailTemplateValidators
{
    public class EmailTemplateSaveDtoValidator : AbstractValidator<EmailTemplateSaveDto>
    {
        public EmailTemplateSaveDtoValidator() 
        {
           
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("El campo 'Subject' es obligatorio.")
                .MaximumLength(200).WithMessage("El campo 'Subject' no puede exceder los 200 caracteres.");
            RuleFor(x => x.TemplateTypeId)
                .NotEmpty().WithMessage("El campo 'TemplateTypeId' es obligatorio.");
            RuleFor(x => x.TenantId)
                .GreaterThan(0).WithMessage("El campo 'TenantId' debe ser mayor a 0");
        }
    }
}
