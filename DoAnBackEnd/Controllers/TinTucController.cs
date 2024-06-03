using DoAn.Service.Constants;
using DoAn.Service.Dtos.NhomDoTuoiDto;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos;
using DoAn.Service.NhomDoTuoiService;
using DoAn.Service.TinTucService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DoAn.Service.Dtos.TinTucDto;
using DoAn.Domain.Entities;
using DoAnBackEnd.Model.TinTucVM;
using DoAn.Service.FileManagerService;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinTucController : ControllerBase
    {
        private readonly ITinTucService _tinTucService;
        private readonly IFileManagerService _fileManagerService;

        public TinTucController
            (
            ITinTucService tinTucService,
            IFileManagerService fileManagerService
            )
        {
            _tinTucService = tinTucService;
            _fileManagerService = fileManagerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataByPage([FromQuery] TinTucSearchDto serach)
        {
            try
            {
                var tintuc = _tinTucService.GetDataByPage(serach);
                if (tintuc.Data.Items != null)
                {
                    var DataTinTuc = tintuc.Data.Items;
                    foreach (var data in DataTinTuc)
                    {
                        var hinhAnh = _fileManagerService.GetFileById(data.Id);
                        var anhDaiDien = hinhAnh.FirstOrDefault()?.Path;
                        data.HinhAnh = anhDaiDien;
                    }

                    string jsonData = JsonConvert.SerializeObject(tintuc.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(TinTucDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, tintuc);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVM tintuc)
        {
            try
            {
                var d = new TinTuc()
                {

                    TieuDe = tintuc.TieuDe,
                    DanhMuc = tintuc.DanhMuc,
                    MoTa = tintuc.MoTa,
                    NoiDung = tintuc.NoiDung,
                    isNoiBat = tintuc.isNoiBat,
                    LuotXem = tintuc.LuotXem,
                };
                await _tinTucService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<TinTuc>()
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
                var rs = new DetailVM();
                rs.objInfo = _tinTucService.GetById(id);
                if (rs.objInfo != null)
                {
                    rs.hinhAnh = _fileManagerService.GetFileById(id);

                }
                return Ok(rs);
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
                    var rs = _tinTucService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.TieuDe = action.TieuDe;
                        rs.DanhMuc = action.DanhMuc;
                        rs.MoTa = action.MoTa;
                        rs.NoiDung = action.NoiDung;
                        rs.isNoiBat = action.isNoiBat;
                        rs.LuotXem = action.LuotXem;

                        await _tinTucService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<TinTuc>
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
                var nhomDoTuoi = _tinTucService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
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
                    await _tinTucService.Delete(nhomDoTuoi);
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
