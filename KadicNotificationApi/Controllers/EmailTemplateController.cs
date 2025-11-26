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
    private readonly IEmailTemplateService _emailTemplateService;
    public EmailTemplateController(IEmailTemplateService emailTemplateSerivice)
    {
        _emailTemplateService = emailTemplateSerivice;
    }


    [HttpGet("Id-TenantId")]
    public async Task<IActionResult> GetById(int id, int tenantId)
    {
        var result = await _emailTemplateService.GetById(id, tenantId);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        if (result.Value == null )
        {
            return NotFound("Registro No Encontrado");
        }
        return Ok(result.Value);
    }
    [HttpGet("TenantId")]
    public async Task<IActionResult> GetByTenant(int tenantId, PaginatorRequestDto paginatorRequestDto)
    {
        var result = await _emailTemplateService.GetByTenant(tenantId, paginatorRequestDto);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        if (result.Value == null || !result.Value.Data.Any())
        {
            return BadRequest("Registro No Encontrado");
        }

        return Ok(result.Value);
    }
    [HttpPost("Save")]
    public async Task<ActionResult> Save([FromBody] EmailTemplateSaveDto emailTemplateSaveDto)
    {
        var result = await _emailTemplateService.Save(emailTemplateSaveDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok(new { message = "Registro guardado con éxito." });
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] EmailTemplateUpdateDto emailTemplateUpdateDto)
    {
        var result = await _emailTemplateService.Update(emailTemplateUpdateDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok(new { message =  "Registro actualizado con éxito." });
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id, int tenantId)
    {
        if (id == null && tenantId == null)
        {
            return BadRequest("Parámetros no válidos, inténtelo nuevamente.");
        }
        var result = await _emailTemplateService.Delete(id, tenantId);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok(new { message = "Registro Eliminado con exito" });
    }
}