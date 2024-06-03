using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;

using DoAn.Service.Dtos;
using DoAn.Service.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.OrderService
{
    public interface IOrderService : IService<Order>
    {
        ResponseWithDataDto<PagedList<OrderDto>> GetDataByPage(OrderSearchDto searchDto);
        ResponseWithDataDto<Order> AddData(OrderDto order);
        ResponseWithMessageDto DeleteData(Guid id);
        ResponseWithMessageDto UpdateData(Guid id, OrderDto order);
        ResponseWithDataDto<OrderDto> FindById(Guid id);
    }
}
