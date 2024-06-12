using DoAn.Service.Constants;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos;
using DoAn.Service.TTSlideService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DoAn.Service.Dtos.TTSlideDto;
using DoAn.Service.FileManagerService;
using DoAn.Domain.Entities;
using DoAnBackEnd.Model.TTSlideVM;
using DoAn.Domain.Migrations;
using DoAn.Service.QLSlideService;


namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTSlideController : ControllerBase
    {
        private readonly ITTSlideService _tTSlideService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IQLSlideService _qLSlideService;

        public TTSlideController(ITTSlideService tTSlideService, IFileManagerService fileManagerService, IQLSlideService qLSlideService) 
        {
            _tTSlideService = tTSlideService;
            _fileManagerService = fileManagerService;
            _qLSlideService = qLSlideService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDataByPage([FromQuery] TTSlideSearchDto search)
        {
            try
            {
                var TTSlide = _tTSlideService.GetDataByPage(search);
                if (TTSlide.Data.Items != null)
                {
                    var Data = TTSlide.Data.Items;

                    if (Data.Count > 0)
                    {
                        foreach (var data in Data)
                        {
                            var hinhAnh = _fileManagerService.GetFileById(data.Id).FirstOrDefault().Path;
                            if (hinhAnh != null)
                            {
                                var anhDaiDien = hinhAnh;
                                data.pathAnh = hinhAnh;
                            }

                        }

                    }

                    string jsonData = JsonConvert.SerializeObject(TTSlide.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(TTSlideDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, TTSlide);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ActionVM action)
        {
            try
            {
                var d = new TTSlide()
                {

                    ThongTin1 = action.ThongTin1,
                    ThongTin2 = action.ThongTin2,
                    ThongTin3 = action.ThongTin3,
                    Type = action.Type,
                    
                };
                await _tTSlideService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<TTSlide>()
                {
                    Status = StatusConstant.SUCCESS,
                    Message = "Thêm sản phẩm thành công",
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
                /*var rs = _sanPhamService.FindById(id);*/

                var model = new Model.TTSlideVM.DetailVM();
                model.objInfo = _tTSlideService.GetById(id);
                if (model.objInfo != null)
                {
                    model.Anh = _fileManagerService.GetFileById(id);

                }

                //return mo.Status == StatusConstant.SUCCESS ? StatusCode(StatusCodes.Status200OK, rs) : StatusCode(StatusCodes.Status500InternalServerError, rs);
                return Ok(model);
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
                    var rs = _tTSlideService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.ThongTin1 = action.ThongTin1;
                        rs.ThongTin2 = action.ThongTin2;
                        rs.ThongTin3 = action.ThongTin3;
                        rs.Type = action.Type;

                        await _tTSlideService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<TTSlide>
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
                            Message = "Không tìm thấy sản phẩm nào"
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
                var thongtin = _tTSlideService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if (thongtin == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto
                    {
                        Message = "Không tìm thấy thông tin",
                        Status = StatusConstant.ERROR
                    });
                }
                else
                {
                    await _tTSlideService.Delete(thongtin);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
                    {
                        Message = "Xóa thành công ",
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
