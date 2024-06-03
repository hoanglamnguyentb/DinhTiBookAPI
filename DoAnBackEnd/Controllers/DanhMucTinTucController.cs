using DoAn.Service.DanhMucTinTucService;
using DoAn.Service.Dtos.DanhMucDto;
using DoAn.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DoAn.Service.Dtos.DanhMucTinTucDto;
using DoAn.Domain.Entities;
using DoAnBackEnd.Model.DanhMucTinTucVM;
using DoAn.Service.Constants;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucTinTucController : ControllerBase
    {
        private readonly IDanhMucTinTucService _danhMucTinTucService;

        public DanhMucTinTucController
            (
                IDanhMucTinTucService danhMucTinTucService
            ) 
        {
            _danhMucTinTucService = danhMucTinTucService;
        }

        [HttpGet("GetDataByPage")]
        public async Task<IActionResult> GetDataByPage([FromQuery] DanhMucTinTucSearchDto serach)
        {
            try
            {
                var danhmuc = _danhMucTinTucService.GetDataByPage(serach);
                if (danhmuc.Data.Items != null)
                {
                    string jsonData = JsonConvert.SerializeObject(danhmuc.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(DanhMucTinTucDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, danhmuc);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVM danhmuc)
        {
            try
            {
                var d = new DanhMucTinTuc()
                {
                    MaDanhMuc = danhmuc.MaDanhMuc,
                    TenDanhMuc = danhmuc.TenDanhMuc,
                    isHienThi = danhmuc.isHienThi,
                   
                };
                await _danhMucTinTucService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<DanhMucTinTuc>()
                {
                    Status = "Success",
                    Message = "Thêm danh mục thành công",
                    Data = d
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = "Error", Message = ex.Message });

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            try
            {
                var rs = _danhMucTinTucService.FindById(id);
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
        public async Task<IActionResult> Edit(Guid id, [FromBody] CreateVM action)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rs = _danhMucTinTucService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.MaDanhMuc = action.MaDanhMuc;
                        rs.TenDanhMuc = action.TenDanhMuc;
                        rs.isHienThi = action.isHienThi;
                       
                        await _danhMucTinTucService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<DanhMucTinTuc>
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
                catch (Exception ex)
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
                var danhmuc = _danhMucTinTucService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if (danhmuc == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto
                    {
                        Message = "Không tìm thấy danh mục",
                        Status = StatusConstant.ERROR
                    });
                }
                else
                {
                    await _danhMucTinTucService.Delete(danhmuc);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
                    {
                        Message = "Xóa thành công danh mục",
                        Status = StatusConstant.SUCCESS
                    });
                }
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
    }
}
