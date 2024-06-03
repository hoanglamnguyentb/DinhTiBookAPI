using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using DoAn.Repository.NhaXuatBanRepository;
using DoAn.Repository.OrderRepository;
using DoAn.Repository.QLSlideRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.NhaXuatBanDto;
using DoAn.Service.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.OrderService
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public ResponseWithDataDto<Order> AddData(OrderDto order)
        {
            try
            {
                var data = _orderRepository.Add(order);
                return data != null ? new ResponseWithDataDto<Order>()
                {
                    Data = data,
                    Message = "Thêm thành công",
                    Status = StatusConstant.SUCCESS,

                } : new ResponseWithDataDto<Order>()
                {
                    Data = null,
                    Message = "Thêm lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<Order>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithMessageDto DeleteData(Guid id)
        {
            try
            {
                var order = _orderRepository.GetById(id);
                if (order == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _orderRepository.Delete(order);
                return new ResponseWithMessageDto()
                {
                    Message = "Xóa thành công",
                    Status = StatusConstant.SUCCESS
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithMessageDto()
                {
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithDataDto<OrderDto> FindById(Guid id)
        {
            try
            {
                var data = from tblOrder in _orderRepository.GetQueryable()
                           where tblOrder.Id == id
                           select new OrderDto
                           {
                               Id = tblOrder.Id,
                               IdUser = tblOrder.IdUser,
                               TenKhachHang = tblOrder.TenKhachHang,
                               NgayDatHang = tblOrder.NgayDatHang,
                               SoDienThoai = tblOrder.SoDienThoai,
                               DiaChi = tblOrder.DiaChi,
                               Tinh = tblOrder.Tinh,
                               Huyen = tblOrder.Huyen,
                               Xa = tblOrder.Xa,
                               TrangThaiDonHang = tblOrder.TrangThaiDonHang,
                               CreatedBy = tblOrder.CreatedBy,
                               CreatedDate = tblOrder.CreatedDate,
                               CreatedID = tblOrder.CreatedID,
                               UpdatedBy = tblOrder.UpdatedBy,
                               UpdatedDate = tblOrder.UpdatedDate,  
                               UpdatedID = tblOrder.UpdatedID,  
                               IsDelete = tblOrder.IsDelete,
                               DeleteTime = tblOrder.DeleteTime,    
                               DeleteBy = tblOrder.DeleteBy,
                               DeleteId = tblOrder.DeleteId,
                           };
                return new ResponseWithDataDto<OrderDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<OrderDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<OrderDto>> GetDataByPage(OrderSearchDto searchDto)
        {
            try
            {
                var query = from tblOrder in _orderRepository.GetQueryable()
                            select new OrderDto
                            {
                                Id = tblOrder.Id,
                                IdUser = tblOrder.IdUser,
                                TenKhachHang = tblOrder.TenKhachHang,
                                NgayDatHang = tblOrder.NgayDatHang,
                                SoDienThoai = tblOrder.SoDienThoai,
                                DiaChi = tblOrder.DiaChi,
                                Tinh = tblOrder.Tinh,
                                Huyen = tblOrder.Huyen,
                                Xa = tblOrder.Xa,
                                TrangThaiDonHang = tblOrder.TrangThaiDonHang,
                                CreatedBy = tblOrder.CreatedBy,
                                CreatedDate = tblOrder.CreatedDate,
                                CreatedID = tblOrder.CreatedID,
                                UpdatedBy = tblOrder.UpdatedBy,
                                UpdatedDate = tblOrder.UpdatedDate,
                                UpdatedID = tblOrder.UpdatedID,
                                IsDelete = tblOrder.IsDelete,
                                DeleteTime = tblOrder.DeleteTime,
                                DeleteBy = tblOrder.DeleteBy,
                                DeleteId = tblOrder.DeleteId,
                            };

                /*if (searchDto != null)
                {
                    if (searchDto.IdUserFilter != null)
                    {
                        query = query.Where(m => m.IdUser == searchDto.IdUserFilter);
                    }
                }*/
                var result = PagedList<OrderDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<OrderDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<OrderDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto UpdateData(Guid id, OrderDto order)
        {
            throw new NotImplementedException();
        }
    }
}
