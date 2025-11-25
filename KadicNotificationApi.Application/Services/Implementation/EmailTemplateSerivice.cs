using FluentValidation;
using KadicNotificationApi.Application.DTOs.DeleteDto;
using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Application.Mappings.GetMappings;
using KadicNotificationApi.Application.Mappings.SaveMappings;
using KadicNotificationApi.Application.Mappings.UpdateMappings;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using KadicTechnology.CommonLib.Paginator;
using KadicTechnology.CommonLib.Utils;
using System.Net;

namespace KadicNotificationApi.Application.Services.Implementation;

public class EmailTemplateSerivice : IEmailTemplateSerivice
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IValidator<EmailTemplateSaveDto> _validatorSaveDto;
    private readonly IValidator<EmailTemplateDeleteDto>_validatorDeleteDto;
    private readonly IValidator<EmailTemplateUpdateDto> _validatorUpdateDto;

    public EmailTemplateSerivice(IEmailTemplateRepository emailTemplateRepository,
        IValidator<EmailTemplateSaveDto> validatorSaveDto,
        IValidator<EmailTemplateDeleteDto> validatorDeleteDto,
        IValidator<EmailTemplateUpdateDto> validatorUpdateDto)
    {
        _emailTemplateRepository = emailTemplateRepository;
        _validatorSaveDto = validatorSaveDto;
        _validatorDeleteDto = validatorDeleteDto;
        _validatorUpdateDto = validatorUpdateDto;
    }
    public async Task<Result<EmailTemplateGetDto>> GetById(int id, int tenantId)
    {
        var template = await _emailTemplateRepository.GetByIdAsync(id, tenantId);
        if (template == null)
        {
            var error = new Error(HttpStatusCode.NotFound, "Registro No Encontrado");
            return Result.Fail<EmailTemplateGetDto>(error);
        }
        var entity = template.ToGetDto();
        return Result.Success(entity);
    }
    public async Task<Result<PaginatorResponseDto<EmailTemplateGetDto>>> GetByTenant(int tenantId, PaginatorRequestDto paginatorRequestDto)
    {
        var pageResult = await _emailTemplateRepository.GetByTenantAsync(tenantId, paginatorRequestDto);
        if (pageResult == null || !pageResult.Data.Any())
        {
            var error = new Error(HttpStatusCode.NotFound, "Registros No Encontrados");
            return Result.Fail<PaginatorResponseDto<EmailTemplateGetDto>>(error);
        }
        var dtoPaginator  = new PaginatorResponseDto<EmailTemplateGetDto>
        {
            Data = pageResult.Data.Select(et => et.ToGetDto()).ToList(),
            PageSize = pageResult.PageSize 
        };
        return Result.Success(dtoPaginator);
    }
    public async Task<Result> Update(EmailTemplateUpdateDto emailTemplateUpdateDto)
    {
        var validationResult = await _validatorUpdateDto.ValidateAsync(emailTemplateUpdateDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var error = new Error(HttpStatusCode.BadRequest, "La solicitud contiene datos inválidos.", errors);
            return Result.Fail(error);
        }
        var existingTemplate = await _emailTemplateRepository.GetByIdAsync(emailTemplateUpdateDto.Id, emailTemplateUpdateDto.TenantId);
        if (existingTemplate == null)
        {
            var error = new Error(HttpStatusCode.NotFound, "Registro No Encontrado");
            return Result.Fail(error);
        }
        var template = emailTemplateUpdateDto.ToEntity();
        await _emailTemplateRepository.UpdateAsync(template);
        return Result.Success();

    }
    public async Task<Result> Save(EmailTemplateSaveDto emailTemplateSaveDto)
    {
        var validationResult = await _validatorSaveDto.ValidateAsync(emailTemplateSaveDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var error = new Error(HttpStatusCode.BadRequest, "La solicitud contiene datos inválidos.", errors);
            return Result.Fail(error);
        }        
        var template = emailTemplateSaveDto.ToEntity();
        await _emailTemplateRepository.SaveAsync(template);
        return Result.Success();
    }   
    public async Task<Result> Delete(EmailTemplateDeleteDto emailTemplateDeleteDto)
    {
      var validationResult = await _validatorDeleteDto.ValidateAsync(emailTemplateDeleteDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var error = new Error(HttpStatusCode.BadRequest, "La solicitud contiene datos inválidos.", errors);
            return Result.Fail(error);
        }
        var template = await _emailTemplateRepository.GetByIdAsync(emailTemplateDeleteDto.Id, emailTemplateDeleteDto.TenantId);
        if (template == null)
        {
            var error = new Error(HttpStatusCode.NotFound, "Registro No Encontrado");
            return Result.Fail(error);
        }
        await _emailTemplateRepository.DeleteAsync(template);
        return Result.Success();
    }
}