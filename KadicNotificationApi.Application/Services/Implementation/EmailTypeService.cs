using FluentValidation;
using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Application.Mappings.GetMappings;
using KadicNotificationApi.Application.Mappings.SaveMappings;
using KadicNotificationApi.Application.Mappings.UpdateMappings;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using KadicTechnology.CommonLib.Paginator;
using KadicTechnology.CommonLib.Utils;
using System.Net;

namespace KadicNotificationApi.Application.Services.Implementation;

public class EmailTypeService : IEmailTypeService
{
    private readonly IEmailTypeRepository _emailTypeRepository;
    private readonly IValidator<PaginatorRequestDto> _paginatorValidator;
    private readonly IValidator<EmailTypeSaveDto> _validatorSaveDto;
    private readonly IValidator<EmailTypeUpdateDto> _validatorUpdateDto;
    
    public EmailTypeService(IEmailTypeRepository emailTypeRepository,
        IValidator<PaginatorRequestDto> paginatorValidator,
        IValidator<EmailTypeSaveDto> validatorSaveDto,
        IValidator<EmailTypeUpdateDto> validatorUpdateDto)
    {
        _emailTypeRepository = emailTypeRepository;
        _paginatorValidator = paginatorValidator;
        _validatorSaveDto = validatorSaveDto;
        _validatorUpdateDto = validatorUpdateDto;
    }

    public async Task<Result<EmailTypeGetDto>> GetById(int id)
    {
        var emailTypeResult = await _emailTypeRepository.GetByIdAsync(id);
        if (emailTypeResult == null)
        {
            var error = new Error(HttpStatusCode.NotFound, GeneralMessages.RecordNotFound);
            return Result.Fail<EmailTypeGetDto>(error);
        }
        var resultDto = emailTypeResult.ToGetDto();
        return Result.Success(resultDto);
    }
    public async Task<Result<PaginatorResponseDto<EmailTypeGetDto>>> GetAll(PaginatorRequestDto paginatorRequestDto)
    {
        var validationResult = await _paginatorValidator.ValidateAsync(paginatorRequestDto);
        var result = new PaginatorResponseDto<EmailTemplateType>();
        var errors = new List<string>();
        var exceptionMessage = "No property";
        var exceptionSyntaxErrorMessage = "Syntax error";
        if (!validationResult.IsValid)
        {
          errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
        try
        {
            if (int.TryParse(paginatorRequestDto.SortField, out var sort))
            {
                var errorMessage = GeneralMessages.InvalidParameter;
                errors.Add(errorMessage);
            }

            result = await _emailTypeRepository.GetAllAsync(paginatorRequestDto);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains(exceptionMessage, StringComparison.CurrentCultureIgnoreCase) || ex.Message.Contains(exceptionSyntaxErrorMessage, StringComparison.CurrentCultureIgnoreCase))
            {
                var errorMessage = GeneralMessages.InvalidParameter;
                errors.Add(errorMessage);
            }
        }
        if (errors.Count > 0)
        {
            var error = new Error(HttpStatusCode.BadRequest, GeneralMessages.InvalidRequest, errors);
            return Result.Fail<PaginatorResponseDto<EmailTypeGetDto>>(error);
        }
        return Result.Success(result.ToDto());

    }
    public async Task<Result> Save(EmailTypeSaveDto emailTypeSaveDto)
    {        
        var validationResult = await _validatorSaveDto.ValidateAsync(emailTypeSaveDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var error = new Error(HttpStatusCode.BadRequest, GeneralMessages.InvalidParameter, errors);
            return Result.Fail(error);
        }        
        var entity = emailTypeSaveDto.ToEntity();        
        await _emailTypeRepository.SaveAsync(entity);
        return Result.Success();
    }
    public async Task<Result> Update(EmailTypeUpdateDto emailTypeUpdateDto)
    {
        var validationResult = await _validatorUpdateDto.ValidateAsync(emailTypeUpdateDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var error = new Error(HttpStatusCode.BadRequest, GeneralMessages.InvalidParameter, errors);
            return Result.Fail(error);
        }
        var existingEntity = await _emailTypeRepository.GetByIdAsync(emailTypeUpdateDto.Id);
        if (existingEntity == null)
        {
            var errors = new List<string> { GeneralMessages.RecordNotFound };
            var error = new Error(HttpStatusCode.NotFound, GeneralMessages.RecordNotFound, errors);
            return Result.Fail(error);
        }
        var entity = emailTypeUpdateDto.ToEntity();        
        await _emailTypeRepository.UpdateAsync(entity);
        return Result.Success();
    }
    public async Task<Result> Delete(int id)
    {
        var existingEntity = await _emailTypeRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            var errors = new List<string> { GeneralMessages.RecordNotFound };
            var error = new Error(HttpStatusCode.NotFound, GeneralMessages.RecordNotFound, errors);
            return Result.Fail(error);
        }
        await _emailTypeRepository.DeleteAsync(id);
        return Result.Success();
    }
}
