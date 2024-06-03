using DoAn.Domain.Entities;
using DoAn.Service.Constants;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.NhaXuatBanDto;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.NhaXuatBanService;
using DoAnBackEnd.Model.NhaXuatBanVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaXuatBanController : ControllerBase
    {
        private readonly INhaXuatBanService _nhaXuatBanService;

        public NhaXuatBanController(INhaXuatBanService nhaXuatBanService) 
        {
            _nhaXuatBanService = nhaXuatBanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataByPage([FromQuery] NhaXuatBanSearchDto serach)
        {
            try
            {
                var nxb = _nhaXuatBanService.GetDataByPage(serach);
                if (nxb.Data.Items != null)
                {
                    string jsonData = JsonConvert.SerializeObject(nxb.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(SanPhamDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, nxb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] NhaXuatBanVM nhaXuatBan)
        {
            try
            {
                var d = new NhaXuatBan()
                {
                    TenNXB = nhaXuatBan.TenNXB,
                    MaNXB = nhaXuatBan.MaNXB,
                };
                await _nhaXuatBanService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<NhaXuatBan>()
                {
                    Status = StatusConstant.SUCCESS,
                    Message = "Thêm  thành công",
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
                var rs = _nhaXuatBanService.FindById(id);
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
        public async Task<IActionResult> Edit(Guid id, [FromBody] NhaXuatBanVM action)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rs = _nhaXuatBanService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.TenNXB = action.TenNXB;
                        rs.MaNXB = action.MaNXB;

                        await _nhaXuatBanService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<NhaXuatBan>
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
                var nhaXuatBan = _nhaXuatBanService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if (nhaXuatBan == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto
                    {
                        Message = "Không tìm thấy NXB",
                        Status = StatusConstant.ERROR
                    });
                }
                else
                {
                    await _nhaXuatBanService.Delete(nhaXuatBan);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
                    {
                        Message = "Xóa thành công NXB",
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
