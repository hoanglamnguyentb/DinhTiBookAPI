using DoAn.Service.AppUserRoleService;
using DoAn.Service.Common;
using DoAn.Service.Dtos.AppRoleDto;
using DoAn.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAn.Service.AppRoleService;
using DoAn.Domain.Entities;
using DoAn.Service.Constants;
using DoAnBackEnd.Model.AppRoleVM;
using HiNet.API.Model.AppRoleVM;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRoleController : ControllerBase
    {
        private readonly IAppRoleService _appRoleService;

        public AppRoleController(IAppRoleService appRoleService) 
        {
            _appRoleService = appRoleService;
        }

        /// <summary>
        /// Danh sách quyền
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Trả về 1 danh sách quyền đã được phân trang và lọc tìm kiếm</returns>
        [HttpGet("get")]
        public ResponseWithDataDto<PagedList<AppRoleDto>> GetAll([FromQuery] AppRoleSearchDto search)
        {
            _ = new ResponseWithDataDto<PagedList<AppRoleDto>>();
            ResponseWithDataDto<PagedList<AppRoleDto>>? data = _appRoleService.GetDataByPage(search);
            return data;
        }

        /// <summary>
        /// Tìm quyền theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = _appRoleService.GetById(id);
                if (data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Dữ liệu không tồn tại" });
                }
                return StatusCode(StatusCodes.Status200OK,
                        new ResponseWithDataDto<AppRole> { Status = StatusConstant.SUCCESS, Data = data, Message = "Tìm dữ liệu thành công" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
               
            }
        }


        /// <summary>
        /// Thêm mới quyền
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Trả về JSON trạng thái và nội dung thông báo</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateVM dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isExist = _appRoleService.CheckExist(dto.Name);
                    if (isExist)
                    {
                        return StatusCode(StatusCodes.Status409Conflict,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tên quyền đã tồn tại" });
                    }
                    var entity = new AppRole() { RoleCode = dto.RoleCode, Name = dto.Name, NormalizedName = dto.Name.ToUpperInvariant() };

                    await _appRoleService.Create(entity);
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

        /// <summary>
        /// Chỉnh sửa thông tin quyền
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="Id"></param>
        /// <returns>Trả về JSON trạng thái và nội dung thông báo</returns>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] EditVM dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = _appRoleService.GetById(id);
                    if (data == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Dữ liệu không tồn tại" });
                    }
                    if (!data.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        bool isExist = _appRoleService.CheckExist(dto.Name);
                        if (isExist)
                        {
                            return StatusCode(StatusCodes.Status409Conflict,
                                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tên quyền đã tồn tại" });
                        }
                    }

                    data.Name = dto.Name;
                    data.RoleCode = dto.RoleCode;
                    data.NormalizedName = dto.Name.ToUpperInvariant();

                    await _appRoleService.Update(data);
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

        /// <summary>
        /// Xoá quyền
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Trả về JSON trạng thái và nội dung thông báo</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var data = _appRoleService.GetById(id);
                if (data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                            new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Dữ liệu không tồn tại" });
                }
                await _appRoleService.Delete(data);
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
