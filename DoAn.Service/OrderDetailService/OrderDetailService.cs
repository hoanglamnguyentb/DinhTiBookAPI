using DoAn.Domain.Entities;
using DoAn.Repository.Core;

using DoAn.Repository.OrderDetailRepository;
using DoAn.Repository.SanPhamRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.NhaXuatBanDto;
using DoAn.Service.Dtos.OrderDetailDto;
using DoAn.Service.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.OrderDetailService
{
    public class OrderDetailService : Service<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ISanPhamRepository _sanPhamRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, ISanPhamRepository sanPhamRepository) : base(orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _sanPhamRepository = sanPhamRepository;
        }

        public ResponseWithDataDto<OrderDetail> Add(OrderDetailDto orderDetail)
        {
            try
            {
                var data = _orderDetailRepository.Add(orderDetail);
                return data != null ? new ResponseWithDataDto<OrderDetail>()
                {
                    Data = data,
                    Message = "Thêm thành công",
                    Status = StatusConstant.SUCCESS,

                } : new ResponseWithDataDto<OrderDetail>()
                {
                    Data = null,
                    Message = "Thêm lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<OrderDetail>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithMessageDto Delete(Guid id)
        {
            try
            {
                var order = _orderDetailRepository.GetById(id);
                if (order == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _orderDetailRepository.Delete(order);
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

        public ResponseWithDataDto<OrderDetailDto> FindById(Guid id)
        {
            try
            {
                var data = from tblOrder in _orderDetailRepository.GetQueryable()
                           where tblOrder.Id == id
                           select new OrderDetailDto
                           {
                               Id = tblOrder.Id,
                               OrderId = tblOrder.OrderId,
                               IdSanPham = tblOrder.IdSanPham,
                               GiaTien = tblOrder.GiaTien,
                               SoLuong = tblOrder.SoLuong,
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
                return new ResponseWithDataDto<OrderDetailDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<OrderDetailDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<OrderDetailDto>> GetDataByPage(OrderDetailSearchDto searchDto)
        {
            try
            {
                var query = from tblOrder in _orderDetailRepository.GetQueryable()
                            select new OrderDetailDto
                            {
                                Id = tblOrder.Id,
                                OrderId = tblOrder.OrderId,
                                IdSanPham = tblOrder.IdSanPham,
                                GiaTien = tblOrder.GiaTien,
                                SoLuong = tblOrder.SoLuong,
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
                    if (searchDto.OrderIdFilter != null)
                    {
                        query = query.Where(m => m.OrderId == searchDto.OrderIdFilter);
                    }
                }*/
                var result = PagedList<OrderDetailDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<OrderDetailDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<OrderDetailDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto Update(Guid id, OrderDetailDto order)
        {
            throw new NotImplementedException();
        }

        public ResponseWithDataDto<List<OrderDetailDto>> GetByOrderId(Guid id)
        {
            try
            {
                var data = from tblOrder in _orderDetailRepository.GetQueryable()
                           where tblOrder.OrderId == id
                join tblSanPham in _sanPhamRepository.GetQueryable()
                           on tblOrder.IdSanPham equals tblSanPham.Id
                           select new OrderDetailDto
                           {
                               Id = tblOrder.Id,
                               OrderId = tblOrder.OrderId,
                               IdSanPham = tblOrder.IdSanPham,
                               GiaTien = tblOrder.GiaTien,
                               SoLuong = tblOrder.SoLuong,
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
                               TenSach = tblSanPham.TenSach,
                           };
                /* return new ResponseWithDataDto<List<OrderDetailDto>>()
                 {
                     Data = data,
                     Status = StatusConstant.SUCCESS,
                     Message = "Lấy thành công"
                 };*/
                var result = data.ToList();
                return new ResponseWithDataDto<List<OrderDetailDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<List<OrderDetailDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }
    }
}
