using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicTechnology.CommonLib.Paginator;
using KadicTechnology.CommonLib.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KadicNotificationApi.Controllers;
[Route("api/[controller]")]

public class EmailTypeController : ControllerBase
{
    private readonly IEmailTypeService _emailTypeService;
    public EmailTypeController(IEmailTypeService emailTypeService)
    {
        _emailTypeService = emailTypeService;
    }
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _emailTypeService.GetById(id);
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
            return BadRequest(GeneralMessages.RecordNotFound);
        }
        return Ok(result.Value);
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginatorRequestDto paginatorRequestDto)
    {
        var result = await _emailTypeService.GetAll(paginatorRequestDto);
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
            return BadRequest(GeneralMessages.RecordNotFound);
        }
        return Ok(result.Value);
    }
    [HttpPost("Save")]
    public async Task<IActionResult> Save([FromBody] EmailTypeSaveDto emailTypeSaveDto)
    {
        if (emailTypeSaveDto == null)
        {
            return BadRequest(GeneralMessages.InvalidRequest);
        }
        var result = await _emailTypeService.Save(emailTypeSaveDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok(GeneralMessages.SaveMessage);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] EmailTypeUpdateDto emailTypeUpdateDto)
    {
        if (emailTypeUpdateDto == null)
        {
            return BadRequest(GeneralMessages.InvalidRequest);
        }
        var result = await _emailTypeService.Update(emailTypeUpdateDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)result.Error.StatusCode, new
            {
                result.Error.Message,
                result.Error.ValidationErrors
            });
        }
        return Ok(GeneralMessages.UpdateMessage);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _emailTypeService.Delete(id);
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
