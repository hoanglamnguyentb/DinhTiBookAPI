using DoAn.Domain.Entities;
using DoAn.Service.AppUserRoleService;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.AppRoleDto;
using DoAn.Service.Dtos.AppUserRoleDto;
using DoAnBackEnd.Model.AppUserRoleVM;
using HiNet.API.Model.AppRoleVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserRoleController : ControllerBase
    {
        private readonly IAppUserRoleService _appUserRoleService;

        public AppUserRoleController(IAppUserRoleService appUserRoleService) 
        {
            _appUserRoleService = appUserRoleService;
        }

        [HttpGet]
        public ResponseWithDataDto<PagedList<AppUserRoleDto>> GetAll([FromQuery] AppUserRoleSearchDto search) 
        {
            _ = new ResponseWithDataDto<PagedList<AppUserRoleDto>>();
            ResponseWithDataDto<PagedList<AppUserRoleDto>>? data = _appUserRoleService.GetDataByPage(search);
            return data;
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = _appUserRoleService.GetById(id);
                if (data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Dữ liệu không tồn tại" });
                }
                return StatusCode(StatusCodes.Status200OK,
                        new ResponseWithDataDto<AppUserRole> { Status = StatusConstant.SUCCESS, Data = data, Message = "Tìm dữ liệu thành công" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });

            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ActionVM dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isExist = _appUserRoleService.CheckExist(dto.UserId, dto.RoleId);
                    if (isExist)
                    {
                        return StatusCode(StatusCodes.Status409Conflict,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = " đã tồn tại" });
                    }
                    var entity = new AppUserRole() { UserId = dto.UserId, RoleId = dto.RoleId };

                    await _appUserRoleService.Create(entity);
                    return StatusCode(StatusCodes.Status201Created,
                            new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Tạo dữ liệu thành công." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
                }

            }
            return StatusCode(StatusCodes.Status400BadRequest,
                   new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Vui lòng điền đầy đủ thông tin yêu cầu." });
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] ActionVM dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = _appUserRoleService.GetById(id);
                    if (data == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Dữ liệu không tồn tại" });
                    }
                    if (!data.RoleId.Equals(dto.RoleId))
                    {
                        bool isExist = _appUserRoleService.CheckExist(dto.UserId, dto.RoleId);
                        if (isExist)
                        {
                            return StatusCode(StatusCodes.Status409Conflict,
                                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tên quyền đã tồn tại" });
                        }
                    }

                    data.UserId = dto.UserId;
                    data.RoleId = dto.RoleId;

                    await _appUserRoleService.Update(data);
                    return StatusCode(StatusCodes.Status200OK,
                            new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Cập nhật dữ liệu thành công." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
                }

            }
            return StatusCode(StatusCodes.Status400BadRequest,
                   new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Vui lòng điền đầy đủ thông tin yêu cầu." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var data = _appUserRoleService.GetById(id);
                if (data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Dữ liệu không tồn tại" });
                }
                await _appUserRoleService.Delete(data);
                return StatusCode(StatusCodes.Status200OK,
                        new ResponseWithDataDto<object> { Status = StatusConstant.SUCCESS, Data = null, Message = "Xoá thành công" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }
    }
}
