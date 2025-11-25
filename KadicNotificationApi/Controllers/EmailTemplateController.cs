using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Application.DTOs.DeleteDto;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicTechnology.CommonLib.Paginator;
using Microsoft.AspNetCore.Mvc;

namespace KadicNotificationApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EmailTemplateController : ControllerBase
{
    private readonly IEmailTemplateSerivice _emailTemplateSerivice;
    public EmailTemplateController(IEmailTemplateSerivice emailTemplateSerivice)
    {
        _emailTemplateSerivice = emailTemplateSerivice;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<EmailTemplateGetDto>> GetById(int id, int tenantId)
    {
        var result = await _emailTemplateSerivice.GetById(id, tenantId);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        if (result.Value == null)
        {
            return NotFound("Registro No Encontrado");
        }
        return Ok(result.Value);
    }
    [HttpGet("Tenant Id")]
    public async Task<ActionResult> GetByTenant(int tenantId, PaginatorRequestDto paginatorRequestDto)
    {
        var result = await _emailTemplateSerivice.GetByTenant(tenantId, paginatorRequestDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok(result.Value);
    }
    [HttpPost]
    public async Task<ActionResult> Save([FromBody] EmailTemplateSaveDto emailTemplateSaveDto)
    {
        var result = await _emailTemplateSerivice.Save(emailTemplateSaveDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok();
    }
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] EmailTemplateUpdateDto emailTemplateUpdateDto)
    {
        var result = await _emailTemplateSerivice.Update(emailTemplateUpdateDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok();
    }
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] EmailTemplateDeleteDto emailTemplateDeleteDto)
    {
        var result = await _emailTemplateSerivice.Delete(emailTemplateDeleteDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok();
    }
}