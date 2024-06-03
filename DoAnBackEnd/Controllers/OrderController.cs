using DoAn.Service.Constants;
using DoAn.Service.Dtos.NhomDoTuoiDto;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos;
using DoAn.Service.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DoAn.Service.Dtos.OrderDto;
using DoAn.Domain.Entities;
using DoAnBackEnd.Model.OrderVM;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataByPage([FromQuery] OrderSearchDto serach)
        {
            try
            {
                var order = _orderService.GetDataByPage(serach);
                if (order.Data.Items != null)
                {
                    string jsonData = JsonConvert.SerializeObject(order.Data.Items);
                    HttpContext.Session.SetString("serachData" + typeof(OrderDto).Name, jsonData);
                }
                return StatusCode(StatusCodes.Status200OK, order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVM order)
        {
            try
            {
                var d = new Order()
                {

                    IdUser = order.IdUser,
                    TenKhachHang = order.TenKhachHang,
                    NgayDatHang = DateTime.Now,
                    SoDienThoai = order.SoDienThoai,
                    DiaChi = order.DiaChi,
                    Tinh = order.Tinh,
                    Huyen = order.Huyen,    
                    Xa = order.Xa,
                    TrangThaiDonHang = order.TrangThaiDonHang,
                };
                await _orderService.Create(d);
                return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<Order>()
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
                var rs = _orderService.FindById(id);
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
                    var rs = _orderService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                    if (rs != null)
                    {
                        rs.TenKhachHang = action.TenKhachHang;
                        rs.SoDienThoai = action.SoDienThoai;
                        rs.DiaChi = action.DiaChi;
                        rs.Tinh = action.Tinh;
                        rs.Huyen = action.Huyen;
                        rs.Xa = action.Xa;
                        rs.TrangThaiDonHang = action.TrangThaiDonHang;

                        await _orderService.Update(rs);
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Order>
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
                var rs = _orderService.FindBy(x => x.Id.Equals(id)).FirstOrDefault();
                if (rs == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseWithMessageDto
                    {
                        Message = "Không tìm thấy thông tin",
                        Status = StatusConstant.ERROR
                    });
                }
                else
                {
                    await _orderService.Delete(rs);
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
