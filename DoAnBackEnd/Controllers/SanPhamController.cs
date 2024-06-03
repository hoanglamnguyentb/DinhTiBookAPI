using DoAn.Service.Dtos.DanhMucDto;
using DoAn.Service.Dtos;
using DoAn.Service.SanPhamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Constants;
using DoAn.Domain.Entities;

using DoAnBackEnd.Model.SanPhamVM;
using DoAn.Service.FileManagerService;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamService _sanPhamService;
        private readonly IFileManagerService _fileManagerService;

        public SanPhamController(ISanPhamService sanPhamService, IFileManagerService fileManagerService) 
        {
            _sanPhamService = sanPhamService;
            _fileManagerService = fileManagerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataByPage([FromQuery] SanPhamSearchDto serach)
        {
            try
            {
                var sanpham = _sanPhamService.GetDataByPage(serach);
                if (sanpham.Data.Items != null)
                {
                    var DataSanpham = sanpham.Data.Items;
                    foreach(var data in DataSanpham)
                    {
                        var hinhAnh = _fileManagerService.GetFileById(data.Id);
                        var anhDaiDien = hinhAnh.FirstOrDefault()?.Path;
                        data.pathAnh = anhDaiDien;
                    }

                    string jsonData = JsonConvert.SerializeObject(sanpham.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(SanPhamDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, sanpham);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }


        [HttpPost("Filter")]
        public async Task<IActionResult> GetDataByPage([FromBody] SanPhamSearchFilterDto search)
        {
            try
            {
                var sanpham = _sanPhamService.GetDataByPageWithFilter(search);
                if (sanpham.Data.Items != null)
                {
                    var DataSanpham = sanpham.Data.Items;
                    foreach (var data in DataSanpham)
                    {
                        var hinhAnh = _fileManagerService.GetFileById(data.Id);
                        var anhDaiDien = hinhAnh.FirstOrDefault()?.Path;
                        data.pathAnh = anhDaiDien;
                    }

                    string jsonData = JsonConvert.SerializeObject(sanpham.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(SanPhamDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, sanpham);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ActionVM sanpham)
        {
            try
            {
                var d = new SanPham()
                {
                    TenSach = sanpham.TenSach,
                    TenTacGia = sanpham.TenTacGia,
                    IdNhaXuatBan = sanpham.IdNhaXuatBan,
                    NamXuatBan = sanpham.NamXuatBan,
                    IdDanhMuc = sanpham.IdDanhMuc,
                    GiaTien = sanpham.GiaTien,
                    SoLuongTon = sanpham.SoLuongTon,
                    MoTaSach = sanpham.MoTaSach,
                    IdNhomDoTuoi = sanpham.IdNhomDoTuoi,
                    NoiBat = sanpham.NoiBat,
                    GiamGia = sanpham.GiamGia,
                    SachKhuyenDoc = sanpham.SachKhuyenDoc,
                    LuotXem = sanpham.LuotXem,
                };
                await _sanPhamService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<SanPham>()
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
        public async Task<IActionResult> FindById(string id)
        {
            try
            {
                /*var rs = _sanPhamService.FindById(id);*/

                var model = new DetailVM();
                model.objInfo = _sanPhamService
                    .FindBy(x => x.Slug == id).FirstOrDefault();
                if(model.objInfo != null)
                {
                    model.AnhSanPham = _fileManagerService.GetFileById(model.objInfo.Id);

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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> FindById(Guid id)
        //{
        //    try
        //    {
        //        /*var rs = _sanPhamService.FindById(id);*/

        //        var model = new DetailVM();
        //        model.objInfo = _sanPhamService.GetById(id);
        //        if (model.objInfo != null)
        //        {
        //            model.AnhSanPham = _fileManagerService.GetFileById(id);

        //        }

        //        //return mo.Status == StatusConstant.SUCCESS ? StatusCode(StatusCodes.Status200OK, rs) : StatusCode(StatusCodes.Status500InternalServerError, rs);
        //        return Ok(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
        //        {
        //            Status = StatusConstant.ERROR,
        //            Message = ex.Message
        //        });

        //    }
        //}

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] ActionVM action)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rs = _sanPhamService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.TenSach = action.TenSach;
                        rs.TenTacGia = action.TenTacGia;
                        rs.IdNhaXuatBan = action.IdNhaXuatBan;
                        rs.NamXuatBan = action.NamXuatBan;
                        rs.IdDanhMuc = action.IdDanhMuc;
                        rs.GiaTien = action.GiaTien;
                        rs.SoLuongTon = action.SoLuongTon;
                        rs.MoTaSach = action.MoTaSach;
                        rs.IdNhomDoTuoi = action.IdNhomDoTuoi;
                        rs.NoiBat = action.NoiBat;
                        rs.GiamGia = action.GiamGia;
                        rs.SachKhuyenDoc = action.SachKhuyenDoc;
                        rs.LuotXem = action.LuotXem;
                        await _sanPhamService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<SanPham>
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
                var sanpham = _sanPhamService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if (sanpham == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto
                    {
                        Message = "Không tìm thấy sản phẩm",
                        Status = StatusConstant.ERROR
                    });
                }
                else
                {
                    await _sanPhamService.Delete(sanpham);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
                    {
                        Message = "Xóa thành công sản phẩm",
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
