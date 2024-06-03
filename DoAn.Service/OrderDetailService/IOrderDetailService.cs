using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;

using DoAn.Service.Dtos;
using DoAn.Service.Dtos.OrderDetailDto;
using DoAn.Service.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.OrderDetailService
{
    public interface IOrderDetailService : IService<OrderDetail>
    {
        ResponseWithDataDto<PagedList<OrderDetailDto>> GetDataByPage(OrderDetailSearchDto searchDto);
        ResponseWithDataDto<OrderDetail> Add(OrderDetailDto orderDetail);
        ResponseWithMessageDto Delete(Guid id);
        ResponseWithMessageDto Update(Guid id, OrderDetailDto orderDetail);
        ResponseWithDataDto<OrderDetailDto> FindById(Guid id);
    }
}
