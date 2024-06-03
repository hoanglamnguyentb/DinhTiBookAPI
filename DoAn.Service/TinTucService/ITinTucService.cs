using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos.OrderDto;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.TinTucDto;

namespace DoAn.Service.TinTucService
{
    public interface ITinTucService : IService<TinTuc>
    {
        ResponseWithDataDto<PagedList<TinTucDto>> GetDataByPage(TinTucSearchDto searchDto);
        ResponseWithMessageDto Delete(Guid id);
        ResponseWithMessageDto Update(Guid id, TinTucDto tintuc);
        ResponseWithDataDto<TinTucDto> FindById(Guid id);
    }
}
