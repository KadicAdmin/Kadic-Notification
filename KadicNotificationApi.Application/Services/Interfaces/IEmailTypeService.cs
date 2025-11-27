using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicTechnology.CommonLib.Paginator;
using KadicTechnology.CommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Application.Services.Interfaces
{
    public interface IEmailTypeService
    {
        public Task<Result<EmailTypeGetDto>> GetById(int id);
        public Task<Result<PaginatorResponseDto<EmailTypeGetDto>>> GetAll(PaginatorRequestDto paginatorRequestDto);
        public Task<Result> Save (EmailTypeSaveDto emailTypeSaveDto);
        public Task<Result> Update (EmailTypeUpdateDto emailTypeUpdateDto);
        public Task<Result> Delete (int id);

    }
}
