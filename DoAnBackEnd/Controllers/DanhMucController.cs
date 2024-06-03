using DoAn.Domain.Entities;
using DoAn.Service.Constants;
using DoAn.Service.DanhMucService;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.DanhMucDto;
using DoAnBackEnd.Model.DanhMucVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucService _danhmucService;

        public DanhMucController(IDanhMucService danhMucService)
        {
            _danhmucService = danhMucService;
        }
        [HttpGet("GetDataByPage")]
        public async Task<IActionResult> GetDataByPage([FromQuery] DanhMucSearchDto serach)
        {
            try
            {
                var danhmuc = _danhmucService.GetDataByPage(serach);
                if(danhmuc.Data.Items != null)
                {
                    string jsonData = JsonConvert.SerializeObject(danhmuc.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(DanhMucDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, danhmuc);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ActionVM danhmuc)
        {
            try
            {
                var d = new DanhMuc()
                {
                    MaCategory = danhmuc.MaCategory,
                    CategoryName = danhmuc.CategoryName,
                    MoTa = danhmuc.MoTa
                };
                await _danhmucService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<DanhMuc>()
                {
                    Status = "Success",
                    Message = "Thêm danh mục thành công",
                    Data = d
                });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = "Error", Message = ex.Message });

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            try
            {
                var rs = _danhmucService.FindById(id);
                return rs.Status == StatusConstant.SUCCESS ? StatusCode(StatusCodes.Status200OK, rs) : StatusCode(StatusCodes.Status500InternalServerError, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto 
                { 
                    Status = StatusConstant.ERROR, 
                    Message = ex.Message 
                });

            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] ActionVM action)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var rs = _danhmucService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.MaCategory = action.MaCategory;
                        rs.CategoryName = action.CategoryName;
                        rs.MoTa = action.MoTa;
                        await _danhmucService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<DanhMuc>
                        {
                            Status = StatusConstant.SUCCESS,
                            Message = "Cập nhật danh mục thành công",
                            Data = rs
                        });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent, new ResponseWithMessageDto
                        {
                            Status = StatusConstant.ERROR,
                            Message = "Không tìm thấy danh mục nào"
                        });
                    }
                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
                    {
                        Status = StatusConstant.ERROR,
                        Message = ex.Message
                    });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
                {
                    Status = StatusConstant.ERROR,
                    Message = "Yêu cầu nhập đầy đủ thông tin"
                });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var danhmuc = _danhmucService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if(danhmuc == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto 
                    { 
                        Message = "Không tìm thấy danh mục", 
                        Status = StatusConstant.ERROR 
                    });
                }
                else
                {
                    await _danhmucService.Delete(danhmuc);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto 
                    { 
                        Message = "Xóa thành công danh mục", 
                        Status = StatusConstant.SUCCESS 
                    });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
                {
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                });
            }
        }



    }
}
