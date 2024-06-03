using DoAn.Service.Constants;
using DoAn.Service.Dtos.NhaXuatBanDto;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos;
using DoAn.Service.NhomDoTuoiService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DoAn.Service.Dtos.NhomDoTuoiDto;
using DoAn.Domain.Entities;

using DoAnBackEnd.Model.NhomDoTuoiVM;
using DoAnBackEnd.Model.NhaXuatBanVM;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomDoTuoiController : ControllerBase
    {
        private readonly INhomDoTuoiService _nhomDoTuoiService;

        public NhomDoTuoiController(INhomDoTuoiService nhomDoTuoiService) 
        {
            _nhomDoTuoiService = nhomDoTuoiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDataByPage([FromQuery] NhomDoTuoiSearchDto serach)
        {
            try
            {
                var nhomDoTuoi = _nhomDoTuoiService.GetDataByPage(serach);
                if (nhomDoTuoi.Data.Items != null)
                {
                    string jsonData = JsonConvert.SerializeObject(nhomDoTuoi.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(SanPhamDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, nhomDoTuoi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVM nhomDoTuoi)
        {
            try
            {
                var d = new NhomDoTuoi()
                {

                    MaNhomDoTuoi = nhomDoTuoi.MaNhomDoTuoi,
                    TenNhom = nhomDoTuoi.TenNhom,
                    MoTaDoTuoi = nhomDoTuoi.MoTaDoTuoi
                };
                await _nhomDoTuoiService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<NhomDoTuoi>()
                {
                    Status = StatusConstant.SUCCESS,
                    Message = "Thêm thành công",
                    Data = d
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            try
            {
                var rs = _nhomDoTuoiService.FindById(id);
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
            if (ModelState.IsValid)
            {
                try
                {
                    var rs = _nhomDoTuoiService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.MaNhomDoTuoi = action.MaNhomDoTuoi;
                        rs.TenNhom = action.TenNhom;
                        rs.MoTaDoTuoi = action.MoTaDoTuoi;

                        await _nhomDoTuoiService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<NhomDoTuoi>
                        {
                            Status = StatusConstant.SUCCESS,
                            Message = "Cập nhật thành công",
                            Data = rs
                        });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent, new ResponseWithMessageDto
                        {
                            Status = StatusConstant.ERROR,
                            Message = "Không tìm thấy NXB"
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
                var nhomDoTuoi = _nhomDoTuoiService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if (nhomDoTuoi == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto
                    {
                        Message = "Không tìm thấy thông tin",
                        Status = StatusConstant.ERROR
                    });
                }
                else
                {
                    await _nhomDoTuoiService.Delete(nhomDoTuoi);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
                    {
                        Message = "Xóa thành công",
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
